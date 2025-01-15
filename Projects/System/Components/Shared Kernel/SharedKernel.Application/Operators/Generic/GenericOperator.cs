using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Enumerations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries;
using SharedKernel.Application.Operators.Generic.UseCases;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Domain.Models.Abstractions.Interfaces;
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
        /// Caché de manejadores registrados para diferentes tipos de operadores.
        /// </summary>
        private static Dictionary<Type, RegisteredHandlers> _handlersCache { get; } = new();

        /// <summary>
        /// Casos de uso relacionados con las entidades.
        /// </summary>
        private Entities_UseCases<EntityType> _useCases { get; }

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
            _useCases = new Entities_UseCases<EntityType>(mainRepository);
            // Establece si se debe realizar un registro detallado de las operaciones.
            _detailedLog = detailedLog;
        }

        #endregion

        #region Handler Management

        /// <summary>
        /// Obtiene los manejadores registrados para un tipo específico de operador.
        /// </summary>
        /// <typeparam name="OperatorType">El tipo de operador para el cual se requieren los manejadores.</typeparam>
        /// <returns>Objeto RegisteredHandlers que contiene los manejadores síncronos y asíncronos.</returns>
        protected static RegisteredHandlers GetHandlers<OperatorType> () {
            // Obtiene el tipo del operador para buscar en el caché.
            var operatorType = typeof(OperatorType);

            // Intenta obtener los manejadores del caché.
            if (!_handlersCache.TryGetValue(operatorType, out var handlers)) {
                // Si no están en caché, registra los manejadores para este tipo.
                handlers = RegisterHandlers<OperatorType>();
                // Almacena los manejadores en caché para futuras consultas.
                _handlersCache[operatorType] = handlers;
            }
            return handlers;
        }

        /// <summary>
        /// Registra los manejadores síncronos y asíncronos para los métodos de un tipo de operador.
        /// </summary>
        /// <typeparam name="OperatorType">El tipo de operador que contiene los métodos a registrar.</typeparam>
        /// <returns>Un objeto RegisteredHandlers con los manejadores registrados.</returns>
        /// <exception cref="InvalidOperationException">Se lanza cuando un método marcado como manejador no cumple con los requisitos.</exception>
        private static RegisteredHandlers RegisterHandlers<OperatorType> () {
            // Inicializa diccionarios para almacenar los manejadores.
            var registeredSynchronousHandlers = new Dictionary<Type, MethodInfo>();
            var registeredAsynchronousHandlers = new Dictionary<Type, MethodInfo>();

            // Obtiene todos los métodos públicos de la clase que tengan el atributo OperationHandler.
            var methods = typeof(OperatorType)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(methodInfo => methodInfo.GetCustomAttribute<OperationHandlerAttribute>() != null);

            // Procesa cada método encontrado.
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

                // Obtiene el tipo del parámetro de la operación.
                var operationType = parameters[0].ParameterType;

                // Valida que el parámetro sea del tipo Operation o un descendiente.
                if (!typeof(Operation).IsAssignableFrom(operationType)) {
                    var methodSignature = $"{method.DeclaringType?.Name}.{method.Name}";
                    var expectedType = typeof(Operation).Name;
                    var actualType = operationType.Name;
                    throw new InvalidOperationException(
                        $"Error de validación en el método '{methodSignature}':\n" +
                        $"- Tipo de parámetro inválido: {actualType}\n" +
                        $"- Tipo esperado: {expectedType} o una clase derivada\n" +
                        "Los métodos marcados con [OperationHandler] deben recibir una operación como parámetro."
                    );
                }

                // Determina si el método es asíncrono basado en su tipo de retorno.
                var isAsyncMethod = method.ReturnType.IsAssignableFrom(typeof(Task));

                // Selecciona el diccionario apropiado según si el método es asíncrono o no.
                var targetDictionary = isAsyncMethod ? registeredAsynchronousHandlers : registeredSynchronousHandlers;

                // Registra el método en el diccionario correspondiente.
                targetDictionary.Add(operationType, method);
            }

            // Crea y retorna el objeto que contiene ambos tipos de manejadores.
            return new RegisteredHandlers(registeredSynchronousHandlers, registeredAsynchronousHandlers);
        }

        #endregion

        #region Permission Validation

        /// <summary>
        /// Valida los permisos necesarios para ejecutar una operación.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de operación a validar.</typeparam>
        /// <param name="operation">La operación que se intenta ejecutar.</param>
        /// <param name="userPermissions">Permisos de usuario extraídos desde el token de acceso proporcionado.</param>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando el usuario no tiene los permisos necesarios.</exception>
        private static void ValidateOperationPermissions<OperationType> (OperationType operation, IEnumerable<Permissions> userPermissions) where OperationType : Operation {
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
            if (!hasRequiredPermission) {
                throw new UnauthorizedAccessException(
                    $"El usuario no tiene los permisos necesarios para ejecutar la operación {operationType.Name}. " +
                    $"Permisos requeridos: {string.Join(", ", requiredPermissionsAttribute.Permissions)}"
                );
            }
        }

        #endregion

        #region Operation Execution

        /// <inheritdoc />
        public Response<ResponseType> ExecuteSynchronousHandler<OperationType, ResponseType> (OperationType operation, TokenClaims tokenClaims) where OperationType : Operation {
            // Valida los permisos antes de ejecutar la operación.
            ValidateOperationPermissions(operation, tokenClaims.Permissions);

            // Obtiene los manejadores registrados para este tipo de operador.
            var handlers = GetHandlers<GenericOperator<EntityType>>();
            // Intenta obtener el manejador correspondiente para el tipo de operación.
            if (!handlers.SynchronousHandlers.TryGetValue(operation.GetType(), out var handler))
                // Si no se encuentra un manejador, lanza una excepción.
                throw new InvalidOperationException($"No hay un manejador síncrono registrado para el tipo de operación {operation.GetType()}.");

            // Invoca el manejador correspondiente y retorna su resultado.
            return (Response<ResponseType>) handler.Invoke(this, [operation])!;
        }

        /// <inheritdoc />
        public async Task<Response<ResponseType>> ExecuteAsynchronousHandler<OperationType, ResponseType> (OperationType operation, TokenClaims tokenClaims) where OperationType : Operation {
            // Valida los permisos antes de ejecutar la operación.
            ValidateOperationPermissions(operation, tokenClaims.Permissions);

            // Obtiene los manejadores registrados para este tipo de operador.
            var handlers = GetHandlers<GenericOperator<EntityType>>();
            // Intenta obtener el manejador correspondiente para el tipo de operación.
            if (!handlers.AsynchronousHandlers.TryGetValue(operation.GetType(), out var handler))
                // Si no se encuentra un manejador, lanza una excepción.
                throw new InvalidOperationException($"No hay un manejador asíncrono registrado para el tipo de operación {operation.GetType()}.");

            // Invoca el manejador correspondiente y espera su resultado.
            return await (Task<Response<ResponseType>>) handler.Invoke(this, [operation])!;
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Agrega una nueva entidad al repositorio.
        /// </summary>
        /// <param name="command">Comando que contiene la entidad a agregar.</param>
        /// <returns>Respuesta que contiene la entidad agregada.</returns>
        public Response<EntityType> AddEntity (AddEntity_Command<EntityType> command) =>
            // Ejecuta el comando de agregar entidad a través del caso de uso correspondiente.
            Executor.ExecuteSynchronousOperation(_useCases.AddEntity.Handle, command, _detailedLog);

        /// <summary>
        /// Obtiene todas las entidades del repositorio.
        /// </summary>
        /// <param name="query">Consulta para obtener las entidades.</param>
        /// <returns>Respuesta que contiene la lista de entidades.</returns>
        public Response<List<EntityType>> GetEntities (GetEntities_Query<EntityType> query) =>
            // Ejecuta la consulta para obtener todas las entidades.
            Executor.ExecuteSynchronousOperation(_useCases.GetEntities.Handle, query, _detailedLog);

        /// <summary>
        /// Obtiene una entidad específica por su ID.
        /// </summary>
        /// <param name="query">Consulta que contiene el ID de la entidad a buscar.</param>
        /// <returns>Respuesta que contiene la entidad encontrada.</returns>
        public Response<EntityType> GetEntityByID (GetEntityByID_Query<EntityType> query) =>
            // Ejecuta la consulta para obtener una entidad por su ID.
            Executor.ExecuteSynchronousOperation(_useCases.GetEntityByID.Handle, query, _detailedLog);

        /// <summary>
        /// Actualiza una entidad existente en el repositorio.
        /// </summary>
        /// <param name="command">Comando que contiene la entidad actualizada.</param>
        /// <returns>Respuesta que contiene la entidad actualizada.</returns>
        public Response<EntityType> UpdateEntity (UpdateEntity_Command<EntityType> command) =>
            // Ejecuta el comando de actualización de la entidad.
            Executor.ExecuteSynchronousOperation(_useCases.UpdateEntity.Handle, command, _detailedLog);

        /// <summary>
        /// Elimina una entidad del repositorio.
        /// </summary>
        /// <param name="command">Comando que contiene el ID de la entidad a eliminar.</param>
        /// <returns>Respuesta que indica el resultado de la operación.</returns>
        public Response<bool> DeleteEntityByID (DeleteEntityByID_Command<EntityType> command) =>
            // Ejecuta el comando de eliminación de la entidad.
            Executor.ExecuteSynchronousOperation(_useCases.DeleteEntityByID.Handle, command, _detailedLog);

        #endregion

        #region Asynchronous CRUD Operations

        /// <summary>
        /// Agrega una nueva entidad al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando que contiene la entidad a agregar.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta con la entidad agregada.</returns>
        public Task<Response<EntityType>> AddEntityAsync (AddEntity_Command<EntityType> command) =>
            // Ejecuta el comando de agregar entidad de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.AddEntity.HandleAsync, command, _detailedLog);

        /// <summary>
        /// Obtiene todas las entidades del repositorio de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener las entidades.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta con la lista de entidades.</returns>
        public Task<Response<List<EntityType>>> GetEntitiesAsync (GetEntities_Query<EntityType> query) =>
            // Ejecuta la consulta para obtener todas las entidades de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.GetEntities.HandleAsync, query, _detailedLog);

        /// <summary>
        /// Obtiene una entidad específica por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta que contiene el ID de la entidad a buscar.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta con la entidad encontrada.</returns>
        public Task<Response<EntityType>> GetEntityByIDAsync (GetEntityByID_Query<EntityType> query) =>
            // Ejecuta la consulta para obtener una entidad por su ID de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.GetEntityByID.HandleAsync, query, _detailedLog);

        /// <summary>
        /// Actualiza una entidad existente en el repositorio de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando que contiene la entidad actualizada.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta con la entidad actualizada.</returns>
        public Task<Response<EntityType>> UpdateEntityAsync (UpdateEntity_Command<EntityType> command) =>
            // Ejecuta el comando de actualización de la entidad de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.UpdateEntity.HandleAsync, command, _detailedLog);

        /// <summary>
        /// Elimina una entidad del repositorio de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando que contiene el ID de la entidad a eliminar.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta que indica el resultado de la operación.</returns>
        public Task<Response<bool>> DeleteEntityByIDAsync (DeleteEntityByID_Command<EntityType> command) =>
            // Ejecuta el comando de eliminación de la entidad de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.DeleteEntityByID.HandleAsync, command, _detailedLog);

        #endregion

    }

}