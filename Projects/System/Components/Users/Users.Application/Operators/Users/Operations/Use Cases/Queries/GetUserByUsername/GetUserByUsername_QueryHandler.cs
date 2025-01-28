using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.Operations.Use_Cases.Queries.GetUserByUsername {

    /// <summary>
    /// Manejador para la consulta de obtención de un usuario por su nombre de usuario.
    /// </summary>
    public class GetUserByUsername_QueryHandler : IOperationHandler<IGetUserByUsername_Query, User> {

        private IUserRepository _userRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByUsername_QueryHandler"/>.
        /// </summary>
        /// <param name="userRepository">Repositorio de usuarios.</param>
        public GetUserByUsername_QueryHandler (IUserRepository userRepository) =>
            _userRepository = userRepository;

        /// <summary>
        /// Maneja la consulta de obtención de un usuario por su nombre de usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de usuario por su nombre de usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el usuario obtenido.</returns>
        public Task<User> Handle (IGetUserByUsername_Query query) => _userRepository.GetUserByUsername(query.Username, query.EnableTracking);

    }

}