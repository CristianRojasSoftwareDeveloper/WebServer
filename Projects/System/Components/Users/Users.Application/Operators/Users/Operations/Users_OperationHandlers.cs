using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using Users.Application.Operators.Users.Operations.CRUD.Commands.UpdateUser;
using Users.Application.Operators.Users.Operations.Use_Cases.Commands.AddRoleToUser;
using Users.Application.Operators.Users.Operations.Use_Cases.Commands.RemoveRoleFromUser;
using Users.Application.Operators.Users.Operations.Use_Cases.Queries.AuthenticateUser;
using Users.Application.Operators.Users.Operations.Use_Cases.Queries.GetUserByToken;
using Users.Application.Operators.Users.Operations.Use_Cases.Queries.GetUserByUsername;

namespace Users.Application.Operators.Users.Operations {

    /// <summary>
    /// Clase que centraliza el acceso a los casos de uso relacionados con los usuarios.
    /// Implementa inicialización lazy para optimizar el rendimiento y la carga de recursos.
    /// </summary>
    public class Users_OperationHandlers {

        #region Queries (Consultas)

        /// <summary>
        /// Caso de uso para autenticar un usuario.
        /// </summary>
        private Lazy<AuthenticateUser_QueryHandler> _authenticateUser { get; }
        public AuthenticateUser_QueryHandler AuthenticateUser => _authenticateUser.Value;

        /// <summary>
        /// Caso de uso para obtener un usuario por su nombre de usuario.
        /// </summary>
        private Lazy<GetUserByUsername_QueryHandler> _getUserByUsername { get; }
        public GetUserByUsername_QueryHandler GetUserByUsername => _getUserByUsername.Value;

        /// <summary>
        /// Caso de uso para obtener un usuario por su token de autenticación.
        /// </summary>
        private Lazy<GetUserByToken_QueryHandler> _getUserByToken { get; }
        public GetUserByToken_QueryHandler GetUserByToken => _getUserByToken.Value;

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Caso de uso para registrar un nuevo usuario.
        /// </summary>
        private Lazy<RegisterUser_CommandHandler> _registerUser { get; }
        public RegisterUser_CommandHandler RegisterUser => _registerUser.Value;

        /// <summary>
        /// Caso de uso para actualizar un usuario existente.
        /// </summary>
        private Lazy<UpdateUser_CommandHandler> _updateUser { get; }
        public UpdateUser_CommandHandler UpdateUser => _updateUser.Value;

        /// <summary>
        /// Caso de uso para agregar un rol a un usuario.
        /// </summary>
        private Lazy<AddRoleToUser_CommandHandler> _addRoleToUser { get; }
        public AddRoleToUser_CommandHandler AddRoleToUser => _addRoleToUser.Value;

        /// <summary>
        /// Caso de uso para eliminar un rol de un usuario.
        /// </summary>
        private Lazy<RemoveRoleFromUser_CommandHandler> _removeRoleFromUser { get; }
        public RemoveRoleFromUser_CommandHandler RemoveRoleFromUser => _removeRoleFromUser.Value;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que inicializa los inicializadores lazy para los casos de uso relacionados con usuarios.
        /// </summary>
        /// <param name="userRepository">Repositorio para las operaciones relacionadas con usuarios.</param>
        /// <param name="roleRepository">Repositorio para las operaciones relacionadas con roles.</param>
        /// <param name="roleAssignedToUserRepository">Repositorio para las asignaciones de roles a usuarios.</param>
        /// <param name="authService">Servicio de autenticación utilizado por los casos de uso.</param>
        public Users_OperationHandlers (IUserRepository userRepository, IRoleRepository roleRepository, IRoleAssignedToUserRepository roleAssignedToUserRepository, IAuthService authService) {
            _authenticateUser = new Lazy<AuthenticateUser_QueryHandler>(() => new AuthenticateUser_QueryHandler(userRepository, authService));
            _getUserByUsername = new Lazy<GetUserByUsername_QueryHandler>(() => new GetUserByUsername_QueryHandler(userRepository));
            _getUserByToken = new Lazy<GetUserByToken_QueryHandler>(() => new GetUserByToken_QueryHandler(userRepository, authService));
            _registerUser = new Lazy<RegisterUser_CommandHandler>(() => new RegisterUser_CommandHandler(userRepository, authService));
            _updateUser = new Lazy<UpdateUser_CommandHandler>(() => new UpdateUser_CommandHandler(userRepository, authService));
            _addRoleToUser = new Lazy<AddRoleToUser_CommandHandler>(() => new AddRoleToUser_CommandHandler(userRepository, roleRepository, roleAssignedToUserRepository));
            _removeRoleFromUser = new Lazy<RemoveRoleFromUser_CommandHandler>(() => new RemoveRoleFromUser_CommandHandler(roleAssignedToUserRepository));
        }

        #endregion

    }

}