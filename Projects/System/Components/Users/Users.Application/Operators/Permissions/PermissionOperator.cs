using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using Users.Application.Operators.Permissions.Operations;

namespace Users.Application.Operators.Permissions {

    /// <summary>
    /// Implementación específica de la interfaz <see cref="IPermissionOperator"/> para operaciones relacionadas con la gestión de los permisos de usuario.
    /// </summary>
    public class PermissionOperator : GenericOperator<Permission>, IPermissionOperator {

        /// <summary>
        /// Casos de uso específicos para permisos.
        /// </summary>
        private Permissions_OperationHandlers _useCases { get; }

        /// <summary>
        /// Constructor que inicializa el operador de permiso de usuario con un repositorio específico de permisos de usuario y roles de permiso.
        /// </summary>
        /// <param name="permissionRepository">El repositorio específico de permisos de usuario.</param>
        /// <param name="permissionAssignedToRoleRepository">El repositorio específico de roles de permiso.</param>
        /// <param name="detailedLog">Indica si se debe utilizar un registro detallado.</param>
        public PermissionOperator (IPermissionRepository permissionRepository, IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository, bool detailedLog = false) : base((IGenericRepository<Permission>) permissionRepository, detailedLog)
            => _useCases = new Permissions_OperationHandlers(permissionRepository, permissionAssignedToRoleRepository);

        #region Métodos asíncronos

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Permission>> AddPermission (IAddPermission_Command command)
            => Executor.ExecuteOperation(_useCases.AddPermission.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<Permission>>> GetPermissions (IGetPermissions_Query query)
            => GetEntities(new GetEntities_Query(query.EnableTracking));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Permission>> GetPermissionByID (IGetPermissionByID_Query query)
            => GetEntityByID(new GetEntityByID_Query(query.ID, query.EnableTracking));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<Permission>>> GetPermissionsByRoleID (IGetPermissionsByRoleID_Query query)
            => Executor.ExecuteOperation(_useCases.GetPermissionsByRoleID.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Permission>> UpdatePermission (IUpdatePermission_Command command)
            => Executor.ExecuteOperation(_useCases.UpdatePermission.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> DeletePermissionByID (IDeletePermissionByID_Command command)
            => DeleteEntityByID(new DeleteEntityByID_Command(command.ID));

        #endregion

    }

}