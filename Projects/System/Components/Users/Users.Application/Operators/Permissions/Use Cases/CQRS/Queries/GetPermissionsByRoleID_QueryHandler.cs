using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.UseCases.Queries;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.UseCases.CQRS.Queries {

    /// <summary>
    /// Manejador para la consulta de obtención de permisos por ID de rol.
    /// </summary>
    public class GetPermissionsByRoleID_QueryHandler : ISyncOperationHandler<GetPermissionsByRoleID_Query, List<Permission>>, IAsyncOperationHandler<GetPermissionsByRoleID_Query, List<Permission>> {

        private IPermissionsAssignedToRoleRepository _permissionAssignedToRoleRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consulta.
        /// </summary>
        /// <param name="permissionAssignedToRoleRepository">El repositorio de roles y permisos.</param>
        public GetPermissionsByRoleID_QueryHandler (IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository) =>
            _permissionAssignedToRoleRepository = permissionAssignedToRoleRepository;

        /// <summary>
        /// Maneja la consulta de obtención de permisos de manera sincrónica.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del rol.</param>
        /// <returns>Una lista de permisos asociados al rol especificado.</returns>
        public List<Permission> Handle (GetPermissionsByRoleID_Query query) {
            var permissionAssignedToRoles = _permissionAssignedToRoleRepository.GetPermissionAssignedToRolesByRoleID(query.RoleID);
            var permissions = permissionAssignedToRoles.Select(permissionAssignedToRole => permissionAssignedToRole.Permission).ToList();
            return permissions;
        }

        /// <summary>
        /// Maneja la consulta de obtención de permisos de manera asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del rol.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene una lista de permisos asociados al rol especificado.</returns>
        public async Task<List<Permission>> HandleAsync (GetPermissionsByRoleID_Query query) {
            var permissionAssignedToRoles = await _permissionAssignedToRoleRepository.GetPermissionAssignedToRolesByRoleIDAsync(query.RoleID);
            var permissions = permissionAssignedToRoles.Select(permissionAssignedToRole => permissionAssignedToRole.Permission).ToList();
            return permissions;
        }

    }

}