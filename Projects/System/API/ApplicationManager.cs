using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SharedKernel.Domain.Models.Entities.Users;
using System.Diagnostics;
using System.Reflection;
using SystemLogs.Application.Operators.SystemLogs;
using SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using Users.Application.Operators.Permissions;
using Users.Application.Operators.Roles;
using Users.Application.Operators.Users;
using Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using Users.Application.Operators.Users.Operations.Use_Cases.Queries.AuthenticateUser;

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
        private IReflexiveOperator GetAssociatedOperator (IOperation operation) {
            // Obtiene el tipo de la operación proporcionada.
            var operationType = operation.GetType();
            // Obtiene el atributo AssociatedOperatorAttribute del tipo de operación.
            var associatedOperatorAttribute = operationType.GetCustomAttribute<AssociatedOperatorAttribute>() ?? throw new InvalidOperationException($"La operación {operationType.Name} no tiene el atributo AssociatedOperator definido.");
            // Extrae la propiedad OperatorType del atributo.
            var operatorType = associatedOperatorAttribute.OperatorType;
            // Busca en el diccionario "_reflexiveOperators" la propiedad correspondiente al tipo de operador especificado.
            // También verifica que el valor obtenido sea una instancia válida de "IReflexiveOperator".
            if (!_reflexiveOperators.TryGetValue(operatorType, out var operatorProperty) || operatorProperty.GetValue(this) is not IReflexiveOperator operatorInstance)
                throw new InvalidOperationException($"No se encontró un operador del tipo {operatorType.Name}.");
            // Devuelve la instancia del operador asociado.
            return operatorInstance;
        }

        /// <summary>
        /// Método genérico utilizado como punto de entrada a la API del sistema, se utiliza para ejecutar operaciones en el mismo.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de la operación a ejecutar.</typeparam>
        /// <typeparam name="ResponseType">El tipo de la respuesta esperada de la operación.</typeparam>
        /// <param name="operation">La operación que se desea ejecutar.</param>
        /// <param name="accessToken">El token de acceso utilizado para validar permisos y tokenClaims.</param>
        /// <returns>Una tarea que representa la respuesta de la operación ejecutada.</returns>
        public async Task<Response<ResponseType>> ExecuteOperation<OperationType, ResponseType> (OperationType operation, string? accessToken = null) where OperationType : IOperation {

            /// Crea un conjunto de tokenClaims para un usuario invitado.
            /// </summary>
            /// <returns>Claims de usuario invitado.</returns>
            static TokenClaims CreateGuestTokenClaims () => new(
                userID: 0,
                username: "Guest",
                email: string.Empty,
                roles: ["Guest"],
                permissions: [SystemPermissions.RegisterUser, SystemPermissions.AuthenticateUser]
            );

            /// <summary>
            /// Agrega detalles al registro del sistema añadiendo información relevante derivada de los token claims del usuario, una operación específica y su respuesta asociada.
            /// </summary>
            /// <param name="systemLog">Instancia de «Entity» que representa el registro del sistema que será actualizado.</param>
            /// <param name="tokenClaims">Instancia de «TokenClaims» que proporciona datos relacionados con el token, como el «ID».</param>
            /// <param name="response">Instancia de «Response<ResponseType>» que contiene los datos obtenidos tras la ejecución de la operación, cuyo formato será evaluado.</param>
            /// <exception cref="InvalidOperationException">Se lanza si la respuesta no cumple con el formato esperado para el tipo de operación ejecutada.</exception>
            void DetailSystemLog (SystemLog systemLog, TokenClaims tokenClaims, OperationType operation, Response<ResponseType>? response) {

                systemLog.UserID = operation switch {
                    // Caso: Operación de registro de usuario.
                    RegisterUser_Command =>
                        // Actualiza el «ID» en el registro del sistema basado en los datos del token o la respuesta.
                        // Si el token contiene un «ID» válido (mayor a 0), se asigna directamente.
                        // En caso contrario, se valida que el cuerpo de la respuesta contenga un objeto de tipo «Entity».
                        // Si ninguna condición es satisfecha, se lanza una excepción indicando un formato inesperado.
                        tokenClaims.UserID switch {
                            > 0 => tokenClaims.UserID, // Caso: «ID» es mayor a 0, se asigna directamente.
                            _ when response != null => response.Body switch {
                                User registeredUser => registeredUser.ID, // Caso: El cuerpo es del tipo «Entity», se asigna su identificador.
                                _ => throw new InvalidOperationException("El cuerpo de la respuesta no es del tipo esperado para «RegisterUser_Command».")
                            }
                        },

                    // Caso: Operación de autenticación de usuario.
                    AuthenticateUser_Query =>
                        // Actualiza el «ID» en el registro del sistema basado en los datos del token o la respuesta.
                        // Si el token contiene un «ID» válido (mayor a 0), se asigna directamente.
                        // En caso contrario, se valida que el cuerpo de la respuesta contenga un token de acceso generado (tipo «string»).
                        // Si el token de acceso es válido, se extrae y valida el «ID» asociado.
                        // Si ninguna condición es satisfecha, se lanza una excepción indicando un formato inesperado.
                        tokenClaims.UserID switch {
                            > 0 => tokenClaims.UserID, // Caso: «ID» es mayor a 0, se asigna directamente.
                            _ when response != null => response.Body switch {
                                string generatedAccessToken => AuthService.ValidateToken(generatedAccessToken).UserID, // Caso: El cuerpo es un token de acceso generado, se valida y se extrae el ID.
                                _ => throw new InvalidOperationException("El cuerpo de la respuesta no es del tipo esperado para «AuthenticateUser_Query».")
                            }
                        },

                    // Caso por defecto.
                    _ => tokenClaims.UserID
                };

                systemLog.Message = $"Operation » {typeof(OperationType).Name}\nArguments: {operation.AsJSON()}\n{systemLog.Message}";

            }

            // Valida que la operación no sea nula, lanzando una excepción si no se cumple.
            if (operation == null)
                throw new ArgumentNullException(nameof(operation), "La operación no puede ser nula.");

            TokenClaims tokenClaims;
            Response<ResponseType>? response = null;

            // Inicia un cronómetro para medir el tiempo de ejecución de la operación.
            var stopwatch = Stopwatch.StartNew();

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

            var systemLog = new SystemLog { Source = $"ApplicationManager.ExecuteOperation" };
            try {
                // Obtiene el operador asociado a la operación especificada.
                var associatedOperator = GetAssociatedOperator(operation);
                // Ejecuta la operación utilizando la función delegada proporcionada.
                response = await associatedOperator.ExecuteHandler<OperationType, ResponseType>(operation, tokenClaims);
                // Crea un log de sistema indicando que la operación se ejecutó exitosamente.
                systemLog.LogLevel = LogLevel.Information;
                // Registra el mensaje de éxito en el log.
                systemLog.Message = "La operación ha sido ejecutada exitosamente.".Underline();
                // Devuelve la respuesta de la operación.
                return response;
            } catch (Exception ex) {
                // Crea un log de sistema indicando que ocurrió un error durante la ejecución.
                systemLog.LogLevel = LogLevel.Error;
                // Registra el mensaje de error en el log.
                systemLog.Message = $"Ha ocurrido un error mientras se ejecutaba la operación: {ex.Message}.".Underline();
                // Lanza nuevamente la excepción para que sea manejada por el invocador.
                throw;
            } finally {
                // Detiene el cronómetro.
                stopwatch.Stop();
                // Registra el tiempo transcurrido.
                systemLog.Message += $"\n{$"Tiempo de ejecución: {stopwatch.Elapsed.AsFormattedTime()}.".Underline()}";
                // Agrega detalles al registro del sistema añadiendo información relevante.
                DetailSystemLog(systemLog, tokenClaims, operation, response);
                // Registra el log utilizando la función delegada proporcionada.
                await SystemLogOperator.AddSystemLog(new AddSystemLog_Command(systemLog));
            }

        }

    }

}