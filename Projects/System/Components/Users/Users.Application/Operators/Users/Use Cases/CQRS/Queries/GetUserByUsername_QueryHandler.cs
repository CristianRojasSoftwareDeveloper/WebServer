using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Queries;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.UseCases.CQRS.Queries {

    /// <summary>
    /// Manejador para la consulta de obtención de un usuario por su nombre de usuario.
    /// </summary>
    public class GetUserByUsername_QueryHandler : ISyncOperationHandler<GetUserByUsername_Query, User>, IAsyncOperationHandler<GetUserByUsername_Query, User> {

        private IUserRepository _userRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByUsername_QueryHandler"/>.
        /// </summary>
        /// <param name="userRepository">Repositorio de usuarios.</param>
        public GetUserByUsername_QueryHandler (IUserRepository userRepository) =>
            _userRepository = userRepository;

        /// <summary>
        /// Maneja la consulta de obtención de un usuario por su nombre de usuario de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de usuario por su nombre de usuario.</param>
        /// <returns>El usuario obtenido.</returns>
        public User Handle (GetUserByUsername_Query query) =>
            _userRepository.GetUserByUsername(query.Username);

        /// <summary>
        /// Maneja la consulta de obtención de un usuario por su nombre de usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de usuario por su nombre de usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el usuario obtenido.</returns>
        public Task<User> HandleAsync (GetUserByUsername_Query query) =>
            _userRepository.GetUserByUsernameAsync(query.Username);

    }

}