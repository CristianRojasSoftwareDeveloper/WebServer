using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;

namespace Users.Application.Operators.Users.Operations.Use_Cases.Queries.AuthenticateUser {

    /// <summary>
    /// Manejador para la consulta de autenticación de usuario.
    /// </summary>
    public class AuthenticateUser_QueryHandler : IOperationHandler<IAuthenticateUser_Query, string> {

        private IAuthService _authService { get; }

        private IUserRepository _userRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de la consulta de autenticación de usuario.
        /// </summary>
        /// <param name="userRepository">El repositorio de usuarios utilizado para acceder a los datos del usuario.</param>
        /// <param name="authService">El servicio de autenticación utilizado para verificar las credenciales del usuario.</param>
        public AuthenticateUser_QueryHandler (IUserRepository userRepository, IAuthService authService) {
            _userRepository = userRepository;
            _authService = authService;
        }

        /// <summary>
        /// Maneja la consulta de autenticación de usuario de forma asíncrona.
        /// </summary>
        /// <param name="authenticateUser_Query">La consulta de autenticación de usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona y el token de acceso generado si la autenticación es exitosa.</returns>
        public async Task<string> Handle (IAuthenticateUser_Query authenticateUser_Query) {
            var user = await _userRepository.GetUserByUsername(authenticateUser_Query.Username) ??
                throw new UnauthorizedAccessException($"No se ha encontrado el usuario con el nombre de usuario «{authenticateUser_Query.Username}»");

            if (!_authService.VerifyPassword(authenticateUser_Query.Password, user.Password!))
                throw new UnauthorizedAccessException("La contraseña es incorrecta");

            return _authService.GenerateToken(user);
        }

    }

}