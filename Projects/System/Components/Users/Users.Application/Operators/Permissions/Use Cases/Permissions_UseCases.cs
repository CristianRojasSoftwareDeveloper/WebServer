using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using Users.Application.Operators.Permissions.UseCases.CQRS.Commands;
using Users.Application.Operators.Permissions.UseCases.CQRS.Queries;

namespace Users.Application.Operators.Permissions.UseCases {

    /// <summary>
    /// Clase que proporciona acceso a todos los casos de uso relacionados con los permisos de usuario.
    /// </summary>
    public class Permissions_UseCases {

        #region Queries
        public GetPermissionsByRoleID_QueryHandler GetPermissionsByRoleID { get; }
        #endregion

        #region Commands
        public AddPermission_CommandHandler AddPermission { get; }
        public UpdatePermission_CommandHandler UpdatePermission { get; }
        #endregion

        /// <summary>
        /// Constructor que inicializa todos los casos de uso con el repositorio de permisos de usuario proporcionado.
        /// </summary>
        /// <param name="permissionAssignedToRoleRepository">El repositorio de la relación entre permisos y roles de usuario utilizado por los casos de uso.</param>
        public Permissions_UseCases (IPermissionRepository permissionRepository, IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository) {
            GetPermissionsByRoleID = new GetPermissionsByRoleID_QueryHandler(permissionAssignedToRoleRepository);
            AddPermission = new AddPermission_CommandHandler(permissionRepository);
            UpdatePermission = new UpdatePermission_CommandHandler(permissionRepository);
        }

    }

}