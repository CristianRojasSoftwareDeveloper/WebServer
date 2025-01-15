using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Queries;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.UseCases.CQRS.Queries {

    /// <summary>
    /// Manejador para la consulta de obtención de roles de usuario por su ID.
    /// </summary>
    public class GetRolesByUserID_QueryHandler : ISyncOperationHandler<GetRolesByUserID_Query, List<Role>>, IAsyncOperationHandler<GetRolesByUserID_Query, List<Role>> {

        private IRoleAssignedToUserRepository _roleAssignedToUserRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de la consulta para obtener roles de usuario por su ID.
        /// </summary>
        /// <param name="roleAssignedToUserRepository">El repositorio de relaciones de usuario y rol.</param>
        public GetRolesByUserID_QueryHandler (IRoleAssignedToUserRepository roleAssignedToUserRepository) =>
            _roleAssignedToUserRepository = roleAssignedToUserRepository;

        /// <summary>
        /// Maneja la consulta para obtener los roles de usuario de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta para obtener los roles de usuario.</param>
        /// <returns>Una lista de roles asociados al usuario.</returns>
        public List<Role> Handle (GetRolesByUserID_Query query) {
            var rolesAssignedToUser = _roleAssignedToUserRepository.GetRolesAssignedToUserByUserID(query.UserID);
            var roles = rolesAssignedToUser.Select(roleAssignedToUser => roleAssignedToUser.Role).ToList();
            return roles;
        }

        /// <summary>
        /// Maneja la consulta para obtener los roles de usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta para obtener los roles de usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona con una lista de roles asociados al usuario.</returns>
        public async Task<List<Role>> HandleAsync (GetRolesByUserID_Query query) {
            var rolesAssignedToUser = await _roleAssignedToUserRepository.GetRolesAssignedToUserByUserIDAsync(query.UserID);
            var roles = rolesAssignedToUser.Select(roleAssignedToUser => roleAssignedToUser.Role).ToList();
            return roles;
        }

    }

}