using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using Users.Application.Operators.Permissions.Operations.CRUD.Commands.AddPermission;
using Users.Application.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission;
using Users.Application.Operators.Permissions.Operations.Use_Cases.Queries.GetPermissionsByRoleID;

namespace Users.Application.Operators.Permissions.Operations {

    /// <summary>
    /// Clase que centraliza el acceso a los casos de uso relacionados con los permisos de usuario.
    /// Implementa inicialización lazy para optimizar el rendimiento.
    /// </summary>
    public class Permissions_OperationHandlers {

        #region Queries (Consultas)

        /// <summary>
        /// Caso de uso para obtener los permisos asignados a un rol específico.
        /// </summary>
        private Lazy<GetPermissionsByRoleID_QueryHandler> _getPermissionsByRoleID { get; }
        public GetPermissionsByRoleID_QueryHandler GetPermissionsByRoleID => _getPermissionsByRoleID.Value;

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Caso de uso para agregar un nuevo permiso.
        /// </summary>
        private Lazy<AddPermission_CommandHandler> _addPermission { get; }
        public AddPermission_CommandHandler AddPermission => _addPermission.Value;

        /// <summary>
        /// Caso de uso para actualizar un permiso existente.
        /// </summary>
        private Lazy<UpdatePermission_CommandHandler> _updatePermission { get; }
        public UpdatePermission_CommandHandler UpdatePermission => _updatePermission.Value;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que inicializa los inicializadores lazy para los casos de uso relacionados con permisos de usuario.
        /// </summary>
        /// <param name="permissionRepository">Repositorio de permisos.</param>
        /// <param name="permissionAssignedToRoleRepository">Repositorio de asignaciones de permisos a roles.</param>
        public Permissions_OperationHandlers (IPermissionRepository permissionRepository, IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository) {
            _getPermissionsByRoleID = new Lazy<GetPermissionsByRoleID_QueryHandler>(() => new GetPermissionsByRoleID_QueryHandler(permissionAssignedToRoleRepository));
            _addPermission = new Lazy<AddPermission_CommandHandler>(() => new AddPermission_CommandHandler(permissionRepository));
            _updatePermission = new Lazy<UpdatePermission_CommandHandler>(() => new UpdatePermission_CommandHandler(permissionRepository));
        }

        #endregion

    }

}