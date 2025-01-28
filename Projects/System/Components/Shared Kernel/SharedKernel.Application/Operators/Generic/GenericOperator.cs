using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Operators.Generic.Operations;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Abstractions.Interfaces;
using System.Collections.Concurrent;
using System.Data;
using System.Reflection;

namespace SharedKernel.Application.Operators.Generic {

    /// <summary>
    /// Implementación abstracta de operaciones genéricas CRUD y manejo de entidades.
    /// Proporciona una base para operadores que trabajan con entidades genéricas.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad con la que se realizarán las operaciones. Debe implementar IGenericEntity.</typeparam>
    /// <remarks>
    /// Esta clase abstracta implementa IGenericOperator y proporciona:
    /// - Manejo automático de operaciones mediante reflexión
    /// - Validación de permisos para operaciones
    /// - Operaciones CRUD básicas para entidades
    /// - Caché de manejadores para mejor rendimiento
    /// </remarks>
    public abstract class GenericOperator<EntityType> : IGenericOperator<EntityType> where EntityType : IGenericEntity {

        #region Properties

        /// <summary>
        /// Caché concurrente de manejadores registrados para diferentes tipos de operadores.
        /// </summary>
        /// <remarks>
        /// Se utiliza un ConcurrentDictionary para garantizar la seguridad en entornos multihilo, 
        /// lo que evita condiciones de carrera y problemas de concurrencia al acceder al caché 
        /// desde múltiples hilos.
        /// </remarks>
        private static readonly ConcurrentDictionary<Type, Dictionary<Type, MethodInfo>> _registerHandlersCache = new();

        /// <summary>
        /// Casos de uso relacionados con las entidades.
        /// </summary>
        private GenericEntities_OperationHandlers<EntityType> _useCases { get; }

        /// <summary>
        /// Indica si se debe realizar un registro detallado de las operaciones.
        /// </summary>
        protected bool _detailedLog { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia del operador genérico.
        /// </summary>
        /// <param name="mainRepository">Repositorio principal que proporciona acceso a los datos de la entidad.</param>
        /// <param name="detailedLog">Indica si se debe activar el registro detallado de operaciones.</param>
        public GenericOperator (IGenericRepository<EntityType> mainRepository, bool detailedLog = false) {
            // Inicializa los casos de uso relacionados con la entidad.
            _useCases = new GenericEntities_OperationHandlers<EntityType>(mainRepository);
            // Establece si se debe realizar un registro detallado de las operaciones.
            _detailedLog = detailedLog;
        }

        #endregion

        #region Permission Validation

        /// <summary>
        /// Valida los permisos necesarios para ejecutar una operación.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de operación a validar.</typeparam>
        /// <param name="operation">La operación que se intenta ejecutar.</param>
        /// <param name="userPermissions">Permisos de usuario extraídos desde el token de acceso proporcionado.</param>
        /// <remarks>
        /// Este método utiliza el atributo `RequiredPermissionsAttribute` para determinar los permisos 
        /// necesarios para ejecutar la operación. Si la operación no tiene el atributo o no requiere 
        /// permisos, se permite la ejecución.
        /// </remarks>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no tiene los permisos necesarios.</exception>
        private static void ValidateOperationPermissions<OperationType> (OperationType operation, IEnumerable<SystemPermissions> userPermissions) where OperationType : IOperation {
            // Valida que la operación no sea nula.
            if (operation == null)
                throw new ArgumentNullException(nameof(operation), "La operación no puede ser nula.");

            // Obtiene el tipo de la operación para acceder a sus atributos.
            var operationType = operation.GetType();

            // Obtiene el atributo de permisos requeridos de la operación.
            var requiredPermissionsAttribute = operationType.GetCustomAttribute<RequiredPermissionsAttribute>();
            // Si no hay permisos requeridos, permite la operación.
            if (requiredPermissionsAttribute == null || !requiredPermissionsAttribute.Permissions.Any())
                return;

            // Verifica si el usuario tiene al menos uno de los permisos requeridos.
            var hasRequiredPermission = requiredPermissionsAttribute.Permissions.Any(userPermissions.HasPermission);
            // Si el usuario no tiene los permisos necesarios, lanza una excepción.
            if (!hasRequiredPermission)
                throw new UnauthorizedAccessException(
                    $"El usuario no tiene los permisos necesarios para ejecutar la operación {operationType.Name}. " +
                    $"Permisos requeridos: {string.Join(", ", requiredPermissionsAttribute.Permissions)}"
                );
        }

        #endregion

        #region Handler Management

        /// <summary>
        /// Registra todos los manejadores de operaciones para un tipo específico de operador.
        /// </summary>
        /// <param name="operatorType">El tipo de operador para el cual se registrarán los manejadores.</param>
        /// <returns>Un diccionario que mapea tipos de operaciones a sus métodos manejadores.</returns>
        /// <remarks>
        /// Este método utiliza reflexión para encontrar y registrar todos los métodos marcados con el atributo
        /// `OperationHandler`.
        /// 
        /// El proceso de reflexión incluye los siguientes pasos:
        /// 1. **Obtener todos los métodos:** Se obtienen todos los métodos públicos de instancia del tipo de operador especificado.
        /// 2. **Filtrar métodos con el atributo:** Se filtran los métodos que tienen el atributo `OperationHandler`.
        /// 3. **Validar y registrar manejadores:** Para cada método encontrado:
        ///     - Se valida que el método tenga exactamente un parámetro del tipo `Operation` o una clase derivada.
        ///     - Si la validación es exitosa, se registra el método como un manejador para el tipo de operación correspondiente.
        /// 
        /// **Manejo de errores:**
        /// - Si se encuentra un método manejador que no cumple con los requisitos de validación, se lanza una excepción `InvalidOperationException`.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Se lanza cuando se encuentra un método marcado como manejador que no cumple con los requisitos:
        /// - Tiene más o menos de un parámetro
        /// - El parámetro no es del tipo Operation o una clase derivada
        /// </exception>
        private static Dictionary<Type, MethodInfo> RegisterHandlers (Type operatorType) {
            // Diccionario para almacenar los manejadores registrados.
            var registeredHandlers = new Dictionary<Type, MethodInfo>();
            // Obtiene todos los métodos públicos de instancia marcados con [OperationHandler].
            var methods = operatorType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(methodInfo => methodInfo.GetCustomAttribute<OperationHandlerAttribute>() != null);

            // Itera sobre cada método encontrado.
            foreach (var method in methods) {
                // Obtiene los parámetros del método.
                var parameters = method.GetParameters();

                // Valida que el método tenga exactamente un parámetro.
                if (parameters.Length != 1) {
                    var methodSignature = $"{method.DeclaringType?.Name}.{method.Name}";
                    throw new InvalidOperationException(
                        $"Error de validación en el método '{methodSignature}':\n" +
                        $"- Se requiere exactamente 1 parámetro\n" +
                        $"- Parámetros encontrados: {parameters.Length}\n" +
                        "Los métodos marcados con [OperationHandler] deben recibir una única operación como parámetro."
                    );
                }

                // Valida que el parámetro sea del tipo Operation o una clase derivada.
                var operationType = parameters[0].ParameterType;
                if (!typeof(IOperation).IsAssignableFrom(operationType)) {
                    var methodSignature = $"{method.DeclaringType?.Name}.{method.Name}";
                    var expectedType = typeof(IOperation).Name;
                    var actualType = operationType.Name;
                    throw new InvalidOperationException(
                        $"Error de validación en el método '{methodSignature}':\n" +
                        $"- Tipo de parámetro inválido: {actualType}\n" +
                        $"- Tipo esperado: {expectedType} o una clase derivada\n" +
                        "Los métodos marcados con [OperationHandler] deben recibir una operación como parámetro."
                    );
                }

                // Agrega el manejador al diccionario.
                registeredHandlers.Add(operationType, method);
            }

            // Devuelve el diccionario de manejadores registrados.
            return registeredHandlers;
        }

        /// <summary>
        /// Obtiene los manejadores de operaciones registrados para el tipo actual del operador.
        /// </summary>
        /// <returns>Un diccionario que mapea tipos de operaciones a sus métodos manejadores correspondientes.</returns>
        /// <remarks>
        /// Este método implementa un mecanismo de caché para optimizar el rendimiento, utilizando un ConcurrentDictionary 
        /// para garantizar la seguridad en entornos multihilo. Los manejadores se registran una única vez por tipo de 
        /// operador y se almacenan en memoria para su reutilización.
        /// 
        /// El método primero intenta obtener los manejadores del caché. Si no se encuentran, se llama al método 
        /// `RegisterHandlers()` para registrar los manejadores utilizando reflexión, y luego se almacenan en el caché 
        /// para futuras llamadas. 
        /// </remarks>
        private Dictionary<Type, MethodInfo> GetHandlers () =>
            // Obtiene los manejadores del operador actual desde el caché concurrente. 
            // Si no existen, se llama a RegisterHandlers para registrarlos y agregarlos al caché.
            _registerHandlersCache.GetOrAdd(GetType(), RegisterHandlers);

        #endregion

        #region Operation Execution

        /// <inheritdoc/>
        public async Task<Response<ResponseType>> ExecuteHandler<OperationType, ResponseType> (OperationType operation, TokenClaims tokenClaims) where OperationType : IOperation {
            // Valida los permisos antes de ejecutar la operación.
            ValidateOperationPermissions(operation, tokenClaims.Permissions);

            // Obtiene los manejadores registrados para este tipo de operador.
            var handlers = GetHandlers();

            var operationType = operation.GetType();
            var operationInterfaces = operationType.GetInterfaces();
            var immediateOperationInterface = operationInterfaces.FirstOrDefault(@interface => @interface.Name.Equals($"I{operationType.Name}")) ??
                throw new InvalidOperationException($"La interface de la operación «{operation.GetType()}» no ha sido encontrada.");

            // Intenta obtener el manejador correspondiente para el tipo de operación.
            if (!handlers.TryGetValue(immediateOperationInterface, out var handler))
                // Si no se encuentra un manejador, lanza una excepción.
                throw new InvalidOperationException($"No hay un manejador asíncrono registrado para la operación «{operation.GetType()}».");

            // Invoca el manejador correspondiente y espera su resultado.
            return await (Task<Response<ResponseType>>) handler.Invoke(this, [operation])!;
        }

        #endregion

        #region Operaciones CRUD asíncronas: Add, Get, Update, Delete

        /// <inheritdoc/>
        public Task<Response<EntityType>> AddEntity (IAddEntity_Command<EntityType> command) =>
            // Ejecuta el comando de agregar entidad de forma asíncrona.
            Executor.ExecuteOperation(_useCases.AddEntity.Handle, command, _detailedLog);

        /// <inheritdoc/>
        public Task<Response<List<EntityType>>> GetEntities (IGetEntities_Query query) =>
            // Ejecuta la consulta para obtener todas las entidades de forma asíncrona.
            Executor.ExecuteOperation(_useCases.GetEntities.Handle, query, _detailedLog);

        /// <inheritdoc/>
        public Task<Response<EntityType>> GetEntityByID (IGetEntityByID_Query query) =>
            // Ejecuta la consulta para obtener una entidad por su ID de forma asíncrona.
            Executor.ExecuteOperation(_useCases.GetEntityByID.Handle, query, _detailedLog);

        /// <inheritdoc/>
        public Task<Response<EntityType>> UpdateEntity (IUpdateEntity_Command<EntityType> command) =>
            // Ejecuta el comando de actualización de la entidad de forma asíncrona.
            Executor.ExecuteOperation(_useCases.UpdateEntity.Handle, command, _detailedLog);

        /// <inheritdoc/>
        public Task<Response<bool>> DeleteEntityByID (IDeleteEntityByID_Command command) =>
            // Ejecuta el comando de eliminación de la entidad de forma asíncrona.
            Executor.ExecuteOperation(_useCases.DeleteEntityByID.Handle, command, _detailedLog);

        #endregion

    }

}