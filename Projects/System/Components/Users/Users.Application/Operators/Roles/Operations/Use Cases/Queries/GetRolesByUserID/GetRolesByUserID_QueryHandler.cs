using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.Use_Cases.Queries.GetRolesByUserID {

    /// <summary>
    /// Manejador para la consulta de obtención de roles de usuario por su ID.
    /// </summary>
    public class GetRolesByUserID_QueryHandler : IOperationHandler<IGetRolesByUserID_Query, List<Role>> {

        private IRoleAssignedToUserRepository _roleAssignedToUserRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de la consulta para obtener roles de usuario por su ID.
        /// </summary>
        /// <param name="roleAssignedToUserRepository">El repositorio de relaciones de usuario y rol.</param>
        public GetRolesByUserID_QueryHandler (IRoleAssignedToUserRepository roleAssignedToUserRepository) =>
            _roleAssignedToUserRepository = roleAssignedToUserRepository;

        /// <summary>
        /// Maneja la consulta para obtener los roles de usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta para obtener los roles de usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona con una lista de roles asociados al usuario.</returns>
        public async Task<List<Role>> Handle (IGetRolesByUserID_Query query) {
            var rolesAssignedToUser = await _roleAssignedToUserRepository.GetRolesAssignedToUserByUserID(query.UserID, query.EnableTracking);
            var roles = rolesAssignedToUser.Select(roleAssignedToUser => roleAssignedToUser.Role).ToList();
            return roles;
        }

    }

}