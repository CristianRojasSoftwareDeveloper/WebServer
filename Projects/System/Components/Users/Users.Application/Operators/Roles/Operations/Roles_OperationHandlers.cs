using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using Users.Application.Operators.Roles.Operations.CRUD.Commands.AddRole;
using Users.Application.Operators.Roles.Operations.CRUD.Commands.UpdateRole;
using Users.Application.Operators.Roles.Operations.Use_Cases.Commands.AddPermissionToRole;
using Users.Application.Operators.Roles.Operations.Use_Cases.Commands.RemovePermissionFromRole;
using Users.Application.Operators.Roles.Operations.Use_Cases.Queries.GetRolesByUserID;

namespace Users.Application.Operators.Roles.Operations {

    /// <summary>
    /// Clase que centraliza el acceso a los casos de uso relacionados con los roles.
    /// Implementa inicialización lazy para optimizar el rendimiento.
    /// </summary>
    public class Roles_OperationHandlers {

        #region Queries (Consultas)

        /// <summary>
        /// Caso de uso para obtener los roles asignados a un usuario por su ID.
        /// </summary>
        private Lazy<GetRolesByUserID_QueryHandler> _getRolesByUserID { get; }
        public GetRolesByUserID_QueryHandler GetRolesByUserID => _getRolesByUserID.Value;

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Caso de uso para agregar un nuevo rol.
        /// </summary>
        private Lazy<AddRole_CommandHandler> _addRole { get; }
        public AddRole_CommandHandler AddRole => _addRole.Value;

        /// <summary>
        /// Caso de uso para actualizar un rol existente.
        /// </summary>
        private Lazy<UpdateRole_CommandHandler> _updateRole { get; }
        public UpdateRole_CommandHandler UpdateRole => _updateRole.Value;

        /// <summary>
        /// Caso de uso para agregar un permiso a un rol.
        /// </summary>
        private Lazy<AddPermissionToRole_CommandHandler> _addPermissionToRole { get; }
        public AddPermissionToRole_CommandHandler AddPermissionToRole => _addPermissionToRole.Value;

        /// <summary>
        /// Caso de uso para eliminar un permiso de un rol.
        /// </summary>
        private Lazy<RemovePermissionFromRole_CommandHandler> _removePermissionFromRole { get; }
        public RemovePermissionFromRole_CommandHandler RemovePermissionFromRole => _removePermissionFromRole.Value;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que inicializa los inicializadores lazy para los casos de uso relacionados con roles.
        /// </summary>
        /// <param name="roleRepository">Repositorio de roles.</param>
        /// <param name="permissionRepository">Repositorio de permisos.</param>
        /// <param name="roleAssignedToUserRepository">Repositorio de asignaciones de roles a usuarios.</param>
        /// <param name="permissionAssignedToRoleRepository">Repositorio de asignaciones de permisos a roles.</param>
        public Roles_OperationHandlers (IRoleRepository roleRepository, IPermissionRepository permissionRepository, IRoleAssignedToUserRepository roleAssignedToUserRepository, IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository) {
            _getRolesByUserID = new Lazy<GetRolesByUserID_QueryHandler>(() => new GetRolesByUserID_QueryHandler(roleAssignedToUserRepository));
            _addRole = new Lazy<AddRole_CommandHandler>(() => new AddRole_CommandHandler(roleRepository));
            _updateRole = new Lazy<UpdateRole_CommandHandler>(() => new UpdateRole_CommandHandler(roleRepository));
            _addPermissionToRole = new Lazy<AddPermissionToRole_CommandHandler>(() => new AddPermissionToRole_CommandHandler(roleRepository, permissionRepository, permissionAssignedToRoleRepository));
            _removePermissionFromRole = new Lazy<RemovePermissionFromRole_CommandHandler>(() => new RemovePermissionFromRole_CommandHandler(permissionAssignedToRoleRepository));
        }

        #endregion

    }

}