using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using Users.Application.Operators.Roles.UseCases.CQRS.Commands;
using Users.Application.Operators.Roles.UseCases.CQRS.Queries;

namespace Users.Application.Operators.Roles.UseCases {

    /// <summary>
    /// Clase que proporciona acceso a todos los casos de uso relacionados con los roles.
    /// </summary>
    public class Roles_UseCases {

        #region Queries
        public GetRolesByUserID_QueryHandler GetRolesByUserID { get; }
        #endregion

        #region Commands
        public AddRole_CommandHandler AddRole { get; }
        public UpdateRole_CommandHandler UpdateRole { get; }
        public AddPermissionToRole_CommandHandler AddPermissionToRole { get; }
        public RemovePermissionFromRole_CommandHandler RemovePermissionFromRole { get; }
        #endregion

        /// <summary>
        /// Constructor que inicializa todos los casos de uso con el repositorio de roles proporcionado.
        /// </summary>
        /// <param name="roleRepository">El repositorio de roles utilizado por los casos de uso.</param>
        public Roles_UseCases (IRoleRepository roleRepository, IPermissionRepository permissionRepository, IRoleAssignedToUserRepository roleAssignedToUserRepository, IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository) {
            GetRolesByUserID = new GetRolesByUserID_QueryHandler(roleAssignedToUserRepository);
            AddRole = new AddRole_CommandHandler(roleRepository);
            UpdateRole = new UpdateRole_CommandHandler(roleRepository);
            AddPermissionToRole = new AddPermissionToRole_CommandHandler(roleRepository, permissionRepository, permissionAssignedToRoleRepository);
            RemovePermissionFromRole = new RemovePermissionFromRole_CommandHandler(permissionAssignedToRoleRepository);
        }

    }

}