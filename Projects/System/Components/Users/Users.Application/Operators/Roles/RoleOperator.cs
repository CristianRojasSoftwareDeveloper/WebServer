using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.Use_Cases.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using Users.Application.Operators.Roles.Operations;

namespace Users.Application.Operators.Roles {

    /// <summary>
    /// Implementación específica de la interfaz <see cref="IRoleOperator"/> para operaciones relacionadas con la gestión de roles de usuario.
    /// </summary>
    public class RoleOperator : GenericOperator<Role>, IRoleOperator {

        private Roles_OperationHandlers _useCases { get; }

        /// <summary>
        /// Constructor que inicializa el operador de role con un repositorio específico de roles.
        /// </summary>
        /// <param name="roleRepository">El repositorio específico de roles.</param>
        public RoleOperator (IRoleRepository roleRepository, IPermissionRepository permissionRepository, IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository, IRoleAssignedToUserRepository roleAssignedToUserRepository, bool detailedLog = false) : base((IGenericRepository<Role>) roleRepository, detailedLog) {
            if (roleRepository is null)
                throw new ArgumentNullException(nameof(roleRepository));
            _useCases = new Roles_OperationHandlers(roleRepository, permissionRepository, roleAssignedToUserRepository, permissionAssignedToRoleRepository);
        }

        #region Métodos asíncronos

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Role>> AddRole (IAddRole_Command command) =>
            Executor.ExecuteOperation(_useCases.AddRole.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<Role>>> GetRoles (IGetRoles_Query query) =>
            GetEntities(new GetEntities_Query(query.EnableTracking));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Role>> GetRoleByID (IGetRoleByID_Query query) =>
            GetEntityByID(new GetEntityByID_Query(query.ID, query.EnableTracking));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<Role>>> GetRolesByUserID (IGetRolesByUserID_Query query) =>
            Executor.ExecuteOperation(_useCases.GetRolesByUserID.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Role>> UpdateRole (IUpdateRole_Command command) =>
            Executor.ExecuteOperation(_useCases.UpdateRole.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> DeleteRoleByID (IDeleteRoleByID_Command command) =>
            DeleteEntityByID(new DeleteEntityByID_Command(command.ID));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<PermissionAssignedToRole>> AddPermissionToRole (IAddPermissionToRole_Command command) =>
            Executor.ExecuteOperation(_useCases.AddPermissionToRole.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> RemovePermissionFromRole (IRemovePermissionFromRole_Command command) =>
            Executor.ExecuteOperation(_useCases.RemovePermissionFromRole.Handle, command, _detailedLog);

        #endregion

    }

}