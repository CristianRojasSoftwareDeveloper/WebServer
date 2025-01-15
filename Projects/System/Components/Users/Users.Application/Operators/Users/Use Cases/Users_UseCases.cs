using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using Users.Application.Operators.Users.UseCases.CQRS.Commands;
using Users.Application.Operators.Users.UseCases.CQRS.Queries;

namespace Users.Application.Operators.Users.UseCases {

    /// <summary>
    /// Clase que proporciona acceso a todos los casos de uso relacionados con los usuarios.
    /// </summary>
    public class Users_UseCases {

        #region Queries
        public AuthenticateUser_QueryHandler AuthenticateUser { get; }
        public GetUserByUsername_QueryHandler GetUserByUsername { get; }
        public GetUserByToken_QueryHandler GetUserByToken { get; }
        #endregion

        #region Commands
        public RegisterUser_CommandHandler RegisterUser { get; }
        public UpdateUser_CommandHandler UpdateUser { get; }
        public AddRoleToUser_CommandHandler AddRoleToUser { get; }
        public RemoveRoleFromUser_CommandHandler RemoveRoleFromUser { get; }
        #endregion

        /// <summary>
        /// Constructor que inicializa todos los casos de uso con el repositorio de usuarios proporcionado.
        /// </summary>
        /// <param name="userRepository">El repositorio de usuarios utilizado por los casos de uso.</param>
        public Users_UseCases (IUserRepository userRepository, IRoleRepository roleRepository, IRoleAssignedToUserRepository roleAssignedToUserRepository, IAuthService authService) {
            AuthenticateUser = new AuthenticateUser_QueryHandler(userRepository, authService);
            GetUserByUsername = new GetUserByUsername_QueryHandler(userRepository);
            GetUserByToken = new GetUserByToken_QueryHandler(userRepository, authService);
            RegisterUser = new RegisterUser_CommandHandler(userRepository, authService);
            UpdateUser = new UpdateUser_CommandHandler(userRepository, authService);
            AddRoleToUser = new AddRoleToUser_CommandHandler(userRepository, roleRepository, roleAssignedToUserRepository);
            RemoveRoleFromUser = new RemoveRoleFromUser_CommandHandler(roleAssignedToUserRepository);
        }

    }

}