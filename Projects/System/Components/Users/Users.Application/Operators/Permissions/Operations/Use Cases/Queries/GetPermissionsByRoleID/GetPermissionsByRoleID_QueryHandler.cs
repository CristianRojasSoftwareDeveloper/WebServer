using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.Operations.Use_Cases.Queries.GetPermissionsByRoleID {

    /// <summary>
    /// Manejador para la consulta de obtención de permisos por ID de rol.
    /// </summary>
    public class GetPermissionsByRoleID_QueryHandler : IOperationHandler<IGetPermissionsByRoleID_Query, List<Permission>> {

        private IPermissionsAssignedToRoleRepository _permissionAssignedToRoleRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consulta.
        /// </summary>
        /// <param name="permissionAssignedToRoleRepository">El repositorio de roles y permisos.</param>
        public GetPermissionsByRoleID_QueryHandler (IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository) =>
            _permissionAssignedToRoleRepository = permissionAssignedToRoleRepository;

        /// <summary>
        /// Maneja la consulta de obtención de permisos de manera asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del rol.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene una lista de permisos asociados al rol especificado.</returns>
        public async Task<List<Permission>> Handle (IGetPermissionsByRoleID_Query query) {
            var permissionAssignedToRoles = await _permissionAssignedToRoleRepository.GetPermissionAssignedToRolesByRoleID(query.RoleID, query.EnableTracking);
            var permissions = permissionAssignedToRoles.Select(permissionAssignedToRole => permissionAssignedToRole.Permission).ToList();
            return permissions;
        }

    }

}