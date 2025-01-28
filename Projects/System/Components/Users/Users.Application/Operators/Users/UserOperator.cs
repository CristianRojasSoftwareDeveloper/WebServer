using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using System.Net;
using Users.Application.Operators.Users.Operations;
using Users.Application.Operators.Users.Operations.Use_Cases.Commands.AddRoleToUser;
using Users.Application.Operators.Users.Operations.Use_Cases.Queries.GetUserByUsername;

namespace Users.Application.Operators.Users {

    /// <summary>
    /// Implementación de la interfaz <see cref="IUserOperator"/> para operaciones relacionadas con usuarios.
    /// </summary>
    public class UserOperator : GenericOperator<User>, IUserOperator {

        private Users_OperationHandlers _useCases { get; }

        /// <summary>
        /// Constructor que inicializa el operador de usuario con un repositorio específico de usuarios.
        /// </summary>
        /// <param name="userRepository">El repositorio específico de usuarios.</param>
        public UserOperator (IUserRepository userRepository, IRoleRepository roleRepository, IRoleAssignedToUserRepository roleAssignedToUserRepository, IAuthService authService, bool detailedLog = false) : base((IGenericRepository<User>) userRepository, detailedLog) =>
            _useCases = new Users_OperationHandlers(userRepository, roleRepository, roleAssignedToUserRepository, authService);

        #region Métodos asíncronos

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<string>> AuthenticateUser (IAuthenticateUser_Query query) =>
            // Ejecuta la autenticación de usuario de forma asíncrona.
            Executor.ExecuteOperation(_useCases.AuthenticateUser.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public async Task<Response<User>> RegisterUser (IRegisterUser_Command command) {
            var operationResponse = await Executor.ExecuteOperation(_useCases.RegisterUser.Handle, command, _detailedLog);
            if (operationResponse == null || operationResponse.Body == null || operationResponse.Body.ID == null)
                return Response<User>.Failure(HttpStatusCode.InternalServerError, $"Ha ocurrido un error durante el registro del usuario: {command.Entity.Username}");

            // Itera sobre los identificadores de roles asociados y agrega cada rol al usuario de forma asíncrona.
            foreach (var roleIdentifier in command.AssociatedRolesIdentifiers)
                await AddRoleToUser(new AddRoleToUser_Command((int) operationResponse.Body.ID, roleIdentifier));

            // Devuelve el usuario registrado utilizando el nombre de usuario de forma asíncrona.
            return await GetUserByUsername(new GetUserByUsername_Query(command.Entity.Username!));
        }

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<User>>> GetUsers (IGetUsers_Query query) =>
            // Obtiene todas las entidades del repositorio de forma asíncrona.
            GetEntities(new GetEntities_Query(query.EnableTracking));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<User>> GetUserByID (IGetUserByID_Query query) =>
            // Obtiene una entidad específica por su ID de forma asíncrona.
            GetEntityByID(new GetEntityByID_Query(query.ID, query.EnableTracking));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<User>> GetUserByUsername (IGetUserByUsername_Query query) =>
            // Ejecuta la consulta para obtener un usuario por su nombre de usuario de forma asíncrona.
            Executor.ExecuteOperation(_useCases.GetUserByUsername.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<User>> GetUserByToken (IGetUserByToken_Query query) =>
            // Ejecuta la consulta para obtener un usuario por su token de forma asíncrona.
            Executor.ExecuteOperation(_useCases.GetUserByToken.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<User>> UpdateUser (IUpdateUser_Command command) =>
            // Ejecuta el comando de actualización de usuario de forma asíncrona.
            Executor.ExecuteOperation(_useCases.UpdateUser.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> DeleteUserByID (IDeleteUserByID_Command command) =>
            // Elimina una entidad del repositorio de forma asíncrona utilizando su ID.
            DeleteEntityByID(new DeleteEntityByID_Command(command.ID));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<RoleAssignedToUser>> AddRoleToUser (IAddRoleToUser_Command command) =>
            // Ejecuta el comando para agregar un rol a un usuario de forma asíncrona.
            Executor.ExecuteOperation(_useCases.AddRoleToUser.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> RemoveRoleFromUser (IRemoveRoleFromUser_Command command) =>
            // Ejecuta el comando para eliminar un rol de un usuario de forma asíncrona.
            Executor.ExecuteOperation(_useCases.RemoveRoleFromUser.Handle, command, _detailedLog);

        #endregion

    }

}