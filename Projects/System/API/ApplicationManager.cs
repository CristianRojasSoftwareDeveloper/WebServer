using SharedKernel.Application.Models.Abstractions;
using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Enumerations;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Commands;
using SharedKernel.Application.Utils.Extensions;
using System.Diagnostics;
using System.Reflection;
using SystemLogs.Application.Operators.SystemLogs;
using Users.Application.Operators.Permissions;
using Users.Application.Operators.Roles;
using Users.Application.Operators.Users;

namespace API {

    /// <summary>
    /// Implementación del administrador de la aplicación que coordina la ejecución de operaciones 
    /// y gestiona los servicios principales del sistema.
    /// </summary>
    public class ApplicationManager : IApplicationManager {

        /// <summary>
        /// Diccionario de propiedades que representan operadores que implementan IReflexiveOperator.
        /// Se utiliza para la introspección de operadores disponibles en el sistema.
        /// </summary>
        private static Dictionary<Type, PropertyInfo> _reflexiveOperators { get; } = typeof(ApplicationManager).GetProperties().
            Where(property => typeof(IReflexiveOperator).IsAssignableFrom(property.PropertyType)).ToDictionary(property => property.PropertyType);

        /// <summary>
        /// Servicio que maneja la autenticación y autorización de usuarios.
        /// </summary>
        public IAuthService AuthService { get; }

        /// <summary>
        /// Servicio que gestiona la persistencia de datos en el sistema.
        /// </summary>
        public IPersistenceService PersistenceService { get; }

        /// <summary>
        /// Operador que maneja las operaciones relacionadas con usuarios.
        /// </summary>
        public IUserOperator UserOperator { get; }

        /// <summary>
        /// Operador que maneja las operaciones relacionadas con roles.
        /// </summary>
        public IRoleOperator RoleOperator { get; }

        /// <summary>
        /// Operador que maneja las operaciones relacionadas con permisos.
        /// </summary>
        public IPermissionOperator PermissionOperator { get; }

        /// <summary>
        /// Operador que maneja las operaciones relacionadas con logs del sistema.
        /// </summary>
        public ISystemLogOperator SystemLogOperator { get; }

        /// <summary>
        /// Constructor de la clase ApplicationManager.
        /// </summary>
        /// <param name="authService">Servicio de autenticación a inyectar.</param>
        /// <param name="persistenceService">Servicio de persistencia a inyectar.</param>
        public ApplicationManager (IAuthService authService, IPersistenceService persistenceService) {
            // Asigna el servicio de autenticación.
            AuthService = authService;
            // Asigna el servicio de persistencia.
            PersistenceService = persistenceService;
            // Inicializa el operador de usuarios con los repositorios necesarios.
            UserOperator = new UserOperator(PersistenceService.UserRepository, PersistenceService.RoleRepository, PersistenceService.RoleAssignedToUserRepository, AuthService);
            // Inicializa el operador de roles con los repositorios necesarios.
            RoleOperator = new RoleOperator(PersistenceService.RoleRepository, PersistenceService.PermissionRepository, PersistenceService.PermissionAssignedToRoleRepository, PersistenceService.RoleAssignedToUserRepository);
            // Inicializa el operador de permisos con los repositorios necesarios.
            PermissionOperator = new PermissionOperator(PersistenceService.PermissionRepository, PersistenceService.PermissionAssignedToRoleRepository);
            // Inicializa el operador de logs del sistema con los repositorios necesarios.
            SystemLogOperator = new SystemLogOperator(PersistenceService.SystemLogRepository, PersistenceService.UserRepository);
        }

        /// <summary>
        /// Obtiene la instancia del operador responsable de procesar una operación específica.
        /// </summary>
        /// <param name="operation">Instancia de la operación que requiere ser procesada.</param>
        /// <returns>Instancia de <see cref="IReflexiveOperator"/> que procesará la operación.</returns>
        /// <exception cref="InvalidOperationException">
        /// Se lanza en los siguientes casos:
        /// <list type="bullet">
        /// <item><description>Cuando la operación no tiene definido el atributo <see cref="AssociatedOperatorAttribute"/>.</description></item>
        /// <item><description>Cuando no se encuentra un operador asociado para el tipo especificado en el atributo.</description></item>
        /// </list>
        /// </exception>
        private IReflexiveOperator GetAssociatedOperator (Operation operation) {
            // Obtiene el tipo de la operación proporcionada.
            var operationType = operation.GetType();

            // Verifica si el tipo de operación tiene definido el atributo "AssociatedOperatorAttribute".
            // Si está definido, extrae la propiedad "OperatorType" del atributo.
            if (operationType.GetCustomAttribute<AssociatedOperatorAttribute>() is not { OperatorType: var operatorType })
                throw new InvalidOperationException($"La operación {operationType.Name} no tiene el atributo AssociatedOperator definido.");

            // Busca en el diccionario "_reflexiveOperators" la propiedad correspondiente al tipo de operador especificado.
            // También verifica que el valor obtenido sea una instancia válida de "IReflexiveOperator".
            if (!_reflexiveOperators.TryGetValue(operatorType, out var operatorProperty) || operatorProperty.GetValue(this) is not IReflexiveOperator operatorInstance)
                throw new InvalidOperationException($"No se encontró un operador del tipo {operatorType.Name}.");

            // Devuelve la instancia del operador asociado.
            return operatorInstance;
        }

        /// <summary>
        /// Método genérico para manejar la ejecución de operaciones, abstracto tanto para operaciones síncronas como asíncronas.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de la operación a ejecutar.</typeparam>
        /// <typeparam name="ResponseType">El tipo de la respuesta esperada de la operación.</typeparam>
        /// <param name="operation">La operación que se desea ejecutar.</param>
        /// <param name="accessToken">El token de acceso utilizado para validar permisos y tokenClaims.</param>
        /// <param name="executeOperation">Función delegada que define cómo ejecutar la operación (síncrona o asíncrona).</param>
        /// <param name="logOperation">Función delegada que define cómo registrar el log (síncrono o asíncrono).</param>
        /// <returns>Una tarea que representa la respuesta de la operación ejecutada.</returns>
        private async Task<Response<ResponseType>> ExecuteOperation<OperationType, ResponseType> (
            OperationType operation,
            string accessToken,
            Func<IReflexiveOperator, OperationType, TokenClaims, Task<Response<ResponseType>>> executeOperation,
            Func<SystemLog, Task> logOperation
        ) where OperationType : Operation {

            /// Crea un conjunto de tokenClaims para un usuario invitado.
            /// </summary>
            /// <returns>Claims de usuario invitado.</returns>
            static TokenClaims CreateGuestTokenClaims () => new(
                userID: 0,
                username: "Guest",
                email: string.Empty,
                roles: ["Guest"],
                permissions: [Permissions.RegisterUser, Permissions.AuthenticateUser]
            );

            // Valida que la operación no sea nula, lanzando una excepción si no se cumple.
            if (operation == null)
                throw new ArgumentNullException(nameof(operation), "La operación no puede ser nula.");

            TokenClaims tokenClaims;

            if (string.IsNullOrWhiteSpace(accessToken))
                tokenClaims = CreateGuestTokenClaims();
            else {
                try {
                    // Valida el token de acceso y obtiene los tokenClaims asociados al usuario.
                    tokenClaims = AuthService.ValidateToken(accessToken);
                } catch (Exception ex) {
                    Console.Error.WriteLine(ex.GetFormattedDetails());
                    return Response<ResponseType>.Failure(UnauthorizedError.Create("El token de acceso proporcionado no es válido o ha expirado"));
                }
            }

            // Inicia un cronómetro para medir el tiempo de ejecución de la operación.
            var stopwatch = Stopwatch.StartNew();

            try {

                // Obtiene el operador asociado a la operación especificada.
                var associatedOperator = GetAssociatedOperator(operation);

                // Ejecuta la operación utilizando la función delegada proporcionada.
                var response = await executeOperation(associatedOperator, operation, tokenClaims);

                // Detiene el cronómetro tras completar la operación.
                stopwatch.Stop();

                // Crea un log de sistema indicando que la operación se ejecutó correctamente.
                var systemLog = new SystemLog(
                    LogLevel.Information,
                    $"ApplicationManager.ExecuteOperation<{nameof(OperationType)},{nameof(ResponseType)}>",
                    $"La operación ha sido ejecutada exitosamente | Tiempo transcurrido: {stopwatch.Elapsed.AsFormattedTime()}."
                );

                // Si el claim del usuario contiene un ID válido, lo agrega al log.
                if (tokenClaims.UserID > 0)
                    systemLog.UserID = tokenClaims.UserID;

                // Registra el log utilizando la función delegada proporcionada.
                await logOperation(systemLog);

                // Devuelve la respuesta de la operación.
                return response;
            } catch (Exception ex) {
                // Detiene el cronómetro en caso de error.
                stopwatch.Stop();

                // Crea un log de sistema indicando que ocurrió un error durante la ejecución.
                var systemLog = new SystemLog(
                    LogLevel.Error,
                    $"ApplicationManager.ExecuteOperation<{nameof(OperationType)},{nameof(ResponseType)}>",
                    $"Ha ocurrido un error mientras se ejecutaba la operación: {ex.Message} | Tiempo transcurrido: {stopwatch.Elapsed.AsFormattedTime()}."
                );

                // Registra el log utilizando la función delegada proporcionada.
                await logOperation(systemLog);

                // Lanza nuevamente la excepción para que sea manejada por el llamador.
                throw;
            } finally {
                stopwatch.Stop();
            }

        }

        /// <inheritdoc/>
        public Response<ResponseType> ExecuteSynchronousOperation<OperationType, ResponseType> (OperationType operation, string? accessToken = null) where OperationType : Operation {
            // Invoca el método genérico, adaptando las funciones delegadas para ejecución síncrona.
            return ExecuteOperation(
                operation,
                accessToken,
                (associatedOperator, operation, tokenClaims) => Task.FromResult(associatedOperator.ExecuteSynchronousHandler<OperationType, ResponseType>(operation, tokenClaims)),
                log => Task.Run(() => SystemLogOperator.AddSystemLog(new AddSystemLog_Command(log)))
            ).GetAwaiter().GetResult(); // Espera el resultado de la tarea para adaptarlo a un flujo síncrono.
        }

        /// <inheritdoc/>
        public Task<Response<ResponseType>> ExecuteAsynchronousOperation<OperationType, ResponseType> (OperationType operation, string? accessToken = null) where OperationType : Operation {
            // Invoca el método genérico, adaptando las funciones delegadas para ejecución asíncrona.
            return ExecuteOperation(
                operation,
                accessToken,
                (associatedOperator, operation, tokenClaims) => associatedOperator.ExecuteAsynchronousHandler<OperationType, ResponseType>(operation, tokenClaims),
                log => SystemLogOperator.AddSystemLogAsync(new AddSystemLog_Command(log))
            );
        }

    }

}