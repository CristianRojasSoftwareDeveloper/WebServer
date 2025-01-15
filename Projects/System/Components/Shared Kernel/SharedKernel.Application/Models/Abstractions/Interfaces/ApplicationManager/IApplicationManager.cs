using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager {

    /// <summary>
    /// Interfaz que define el administrador de la aplicación, proporcionando acceso a los servicios y operadores principales.
    /// </summary>
    public interface IApplicationManager {

        /// <summary>
        /// Servicio de autenticación que maneja la autenticación y autorización de usuarios en el sistema.
        /// </summary>
        IAuthService AuthService { get; }

        /// <summary>
        /// Servicio de persistencia que proporciona una interfaz para interactuar con el almacenamiento de datos.
        /// </summary>
        IPersistenceService PersistenceService { get; }

        /// <summary>
        /// Operador de usuarios que orquesta los servicios de infraestructura para realizar operaciones relacionadas con usuarios, como crear, leer, actualizar y eliminar usuarios.
        /// </summary>
        IUserOperator UserOperator { get; }

        /// <summary>
        /// Operador de roles de usuario que orquesta los servicios de infraestructura para realizar operaciones relacionadas con roles, como crear, leer, actualizar y eliminar roles de usuario.
        /// </summary>
        IRoleOperator RoleOperator { get; }

        /// <summary>
        /// Operador de permisos de roles de usuario que orquesta los servicios de infraestructura para realizar operaciones relacionadas con permisos, como crear, leer, actualizar y eliminar permisos de roles de usuario.
        /// </summary>
        IPermissionOperator PermissionOperator { get; }

        /// <summary>
        /// Operador de registros del sistema que orquesta los servicios de infraestructura para realizar operaciones relacionadas con registros, como crear, leer, actualizar y eliminar registros del sistema.
        /// </summary>
        ISystemLogOperator SystemLogOperator { get; }

        /// <summary>
        /// Ejecuta una operación de manera síncrona.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de la operación a ejecutar.</typeparam>
        /// <typeparam name="ResponseType">El tipo de la respuesta esperada de la operación.</typeparam>
        /// <param name="operation">La operación que se desea ejecutar.</param>
        /// <param name="accessToken">El token de acceso utilizado para validar permisos y claims.</param>
        /// <returns>La respuesta de la operación ejecutada.</returns>
        Response<ResponseType> ExecuteSynchronousOperation<OperationType, ResponseType> (OperationType operation, string? accessToken = null) where OperationType : Operation;

        /// <summary>
        /// Ejecuta una operación de manera asíncrona.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de la operación a ejecutar.</typeparam>
        /// <typeparam name="ResponseType">El tipo de la respuesta esperada de la operación.</typeparam>
        /// <param name="operation">La operación que se desea ejecutar.</param>
        /// <param name="accessToken">El token de acceso utilizado para validar permisos y claims.</param>
        /// <returns>Una tarea que representa la respuesta de la operación ejecutada.</returns>
        Task<Response<ResponseType>> ExecuteAsynchronousOperation<OperationType, ResponseType> (OperationType operation, string? accessToken = null) where OperationType : Operation;

    }

}