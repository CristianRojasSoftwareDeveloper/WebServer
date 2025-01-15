using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Queries;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.UseCases.CQRS.Queries {

    /// <summary>
    /// Manejador para la consulta de obtención de usuario mediante un token de autenticación.
    /// </summary>
    public class GetUserByToken_QueryHandler : ISyncOperationHandler<GetUserByToken_Query, User>, IAsyncOperationHandler<GetUserByToken_Query, User> {

        private IUserRepository _userRepository { get; }
        private IAuthService _authService { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByToken_QueryHandler"/>.
        /// </summary>
        /// <param name="userRepository">Repositorio de usuarios.</param>
        /// <param name="authService">Servicio de autenticación.</param>
        public GetUserByToken_QueryHandler (IUserRepository userRepository, IAuthService authService) {
            _userRepository = userRepository;
            _authService = authService;
        }

        /// <summary>
        /// Maneja la consulta de obtención de usuario mediante un token de autenticación de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de usuario mediante un token de autenticación.</param>
        /// <returns>El usuario obtenido.</returns>
        public User Handle (GetUserByToken_Query query) {
            // Verificar que la consulta no sea nula
            ArgumentNullException.ThrowIfNull(query);

            // Verificar que el token no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(query.Token))
                throw BadRequestError.Create("El token no puede ser nulo o vacío");

            // Validar el token y obtener los atributos
            var tokenClaims = _authService.ValidateToken(query.Token);

            // Verifica si el atributo de nombre de usuario está presente en el token.
            if (string.IsNullOrWhiteSpace(tokenClaims.Username))
                // Si el atributo de nombre de usuario no está presente en el token, se lanza un error.
                throw ValidationError.Create(nameof(tokenClaims.Username), "No se ha encontrado el atributo del nombre de usuario dentro del token");

            // Obtiene y retorna el usuario por su nombre de usuario.
            return _userRepository.GetUserByUsername(tokenClaims.Username);
        }

        /// <summary>
        /// Maneja la consulta de obtención de usuario mediante un token de autenticación de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de usuario mediante un token de autenticación.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el usuario obtenido.</returns>
        public Task<User> HandleAsync (GetUserByToken_Query query) {
            // Verificar que la consulta no sea nula
            ArgumentNullException.ThrowIfNull(query);

            // Verificar que el token no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(query.Token))
                throw BadRequestError.Create("El token no puede ser nulo o vacío");

            // Validar el token y obtener los atributos
            var tokenClaims = _authService.ValidateToken(query.Token);

            // Verifica si el atributo de nombre de usuario está presente en el token.
            if (string.IsNullOrWhiteSpace(tokenClaims.Username))
                // Si el atributo de nombre de usuario no está presente en el token, se lanza un error.
                throw ValidationError.Create(nameof(tokenClaims.Username), "No se ha encontrado el atributo del nombre de usuario dentro del token");

            // Obtiene y retorna el usuario por su nombre de usuario.
            return _userRepository.GetUserByUsernameAsync(tokenClaims.Username);
        }

    }

}