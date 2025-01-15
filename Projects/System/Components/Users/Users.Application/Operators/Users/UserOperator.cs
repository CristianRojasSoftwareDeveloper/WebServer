using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Queries;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using System.Net;
using Users.Application.Operators.Users.UseCases;

namespace Users.Application.Operators.Users {

    /// <summary>
    /// Implementación de la interfaz <see cref="IUserOperator"/> para operaciones relacionadas con usuarios.
    /// </summary>
    public class UserOperator : GenericOperator<User>, IUserOperator {

        private Users_UseCases _useCases { get; }

        /// <summary>
        /// Constructor que inicializa el operador de usuario con un repositorio específico de usuarios.
        /// </summary>
        /// <param name="userRepository">El repositorio específico de usuarios.</param>
        public UserOperator (IUserRepository userRepository, IRoleRepository roleRepository, IRoleAssignedToUserRepository roleAssignedToUserRepository, IAuthService authService, bool detailedLog = false) : base((IGenericRepository<User>) userRepository, detailedLog) =>
            _useCases = new Users_UseCases(userRepository, roleRepository, roleAssignedToUserRepository, authService);

        #region Métodos síncronos

        /// <inheritdoc />
        [OperationHandler]
        public Response<string> AuthenticateUser (AuthenticateUser_Query query) =>
            Executor.ExecuteSynchronousOperation(_useCases.AuthenticateUser.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<User> RegisterUser (RegisterUser_Command command) {
            var operationResponse = Executor.ExecuteSynchronousOperation(_useCases.RegisterUser.Handle, command, _detailedLog);
            if (operationResponse == null || operationResponse.Body == null || operationResponse.Body.ID == null)
                return Response<User>.Failure(HttpStatusCode.InternalServerError, $"Ha ocurrido un error durante el registro del usuario: {command.User.Username}");

            // Itera sobre los identificadores de roles asociados y agrega cada rol al usuario.
            foreach (var roleIdentifier in command.AssociatedRolesIdentifiers)
                AddRoleToUser(new AddRoleToUser_Command((int) operationResponse.Body.ID, roleIdentifier));

            // Devuelve el usuario registrado utilizando el nombre de usuario.
            return GetUserByUsername(new GetUserByUsername_Query(command.User.Username!));
        }

        /// <inheritdoc />
        [OperationHandler]
        public Response<List<User>> GetUsers (GetUsers_Query query) =>
            // Obtiene todas las entidades del repositorio.
            GetEntities(new GetEntities_Query<User>());

        /// <inheritdoc />
        [OperationHandler]
        public Response<User> GetUserByID (GetUserByID_Query query) =>
            // Obtiene una entidad específica por su ID.
            GetEntityByID(new GetEntityByID_Query<User>(query.UserID));

        /// <inheritdoc />
        [OperationHandler]
        public Response<User> GetUserByUsername (GetUserByUsername_Query query) =>
            // Ejecuta la consulta para obtener un usuario por su nombre de usuario.
            Executor.ExecuteSynchronousOperation(_useCases.GetUserByUsername.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<User> GetUserByToken (GetUserByToken_Query query) =>
            // Ejecuta la consulta para obtener un usuario por su token.
            Executor.ExecuteSynchronousOperation(_useCases.GetUserByToken.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<User> UpdateUser (UpdateUser_Command command) =>
            // Ejecuta el comando de actualización de usuario.
            Executor.ExecuteSynchronousOperation(_useCases.UpdateUser.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<bool> DeleteUserByID (DeleteUserByID_Command command) =>
            // Elimina una entidad del repositorio utilizando su ID.
            DeleteEntityByID(new DeleteEntityByID_Command<User>(command.UserID));

        /// <inheritdoc />
        [OperationHandler]
        public Response<RoleAssignedToUser> AddRoleToUser (AddRoleToUser_Command command) =>
            // Ejecuta el comando para agregar un rol a un usuario.
            Executor.ExecuteSynchronousOperation(_useCases.AddRoleToUser.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<bool> RemoveRoleFromUser (RemoveRoleFromUser_Command command) =>
            // Ejecuta el comando para eliminar un rol de un usuario.
            Executor.ExecuteSynchronousOperation(_useCases.RemoveRoleFromUser.Handle, command, _detailedLog);

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<string>> AuthenticateUserAsync (AuthenticateUser_Query query) =>
            // Ejecuta la autenticación de usuario de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.AuthenticateUser.HandleAsync, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public async Task<Response<User>> RegisterUserAsync (RegisterUser_Command command) {
            var operationResponse = await Executor.ExecuteAsynchronousOperation(_useCases.RegisterUser.HandleAsync, command, _detailedLog);
            if (operationResponse == null || operationResponse.Body == null || operationResponse.Body.ID == null)
                return Response<User>.Failure(HttpStatusCode.InternalServerError, $"Ha ocurrido un error durante el registro del usuario: {command.User.Username}");

            // Itera sobre los identificadores de roles asociados y agrega cada rol al usuario de forma asíncrona.
            foreach (var roleIdentifier in command.AssociatedRolesIdentifiers)
                await AddRoleToUserAsync(new AddRoleToUser_Command((int) operationResponse.Body.ID, roleIdentifier));

            // Devuelve el usuario registrado utilizando el nombre de usuario de forma asíncrona.
            return await GetUserByUsernameAsync(new GetUserByUsername_Query(command.User.Username!));
        }

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<User>>> GetUsersAsync (GetUsers_Query query) =>
            // Obtiene todas las entidades del repositorio de forma asíncrona.
            GetEntitiesAsync(new GetEntities_Query<User>());

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<User>> GetUserByIDAsync (GetUserByID_Query query) =>
            // Obtiene una entidad específica por su ID de forma asíncrona.
            GetEntityByIDAsync(new GetEntityByID_Query<User>(query.UserID));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<User>> GetUserByUsernameAsync (GetUserByUsername_Query query) =>
            // Ejecuta la consulta para obtener un usuario por su nombre de usuario de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.GetUserByUsername.HandleAsync, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<User>> GetUserByTokenAsync (GetUserByToken_Query query) =>
            // Ejecuta la consulta para obtener un usuario por su token de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.GetUserByToken.HandleAsync, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<User>> UpdateUserAsync (UpdateUser_Command command) =>
            // Ejecuta el comando de actualización de usuario de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.UpdateUser.HandleAsync, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> DeleteUserByIDAsync (DeleteUserByID_Command command) =>
            // Elimina una entidad del repositorio de forma asíncrona utilizando su ID.
            DeleteEntityByIDAsync(new DeleteEntityByID_Command<User>(command.UserID));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<RoleAssignedToUser>> AddRoleToUserAsync (AddRoleToUser_Command command) =>
            // Ejecuta el comando para agregar un rol a un usuario de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.AddRoleToUser.HandleAsync, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> RemoveRoleFromUserAsync (RemoveRoleFromUser_Command command) =>
            // Ejecuta el comando para eliminar un rol de un usuario de forma asíncrona.
            Executor.ExecuteAsynchronousOperation(_useCases.RemoveRoleFromUser.HandleAsync, command, _detailedLog);

        #endregion

    }

}