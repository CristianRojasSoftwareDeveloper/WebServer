using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Queries;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using Users.Application.Operators.Roles.UseCases;

namespace Users.Application.Operators.Roles {

    /// <summary>
    /// Implementación específica de la interfaz <see cref="IRoleOperator"/> para operaciones relacionadas con la gestión de roles de usuario.
    /// </summary>
    public class RoleOperator : GenericOperator<Role>, IRoleOperator {

        private Roles_UseCases _useCases { get; }

        /// <summary>
        /// Constructor que inicializa el operador de role con un repositorio específico de roles.
        /// </summary>
        /// <param name="roleRepository">El repositorio específico de roles.</param>
        public RoleOperator (IRoleRepository roleRepository, IPermissionRepository permissionRepository, IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository, IRoleAssignedToUserRepository roleAssignedToUserRepository, bool detailedLog = false) : base((IGenericRepository<Role>) roleRepository, detailedLog) {
            if (roleRepository is null)
                throw new ArgumentNullException(nameof(roleRepository));
            _useCases = new Roles_UseCases(roleRepository, permissionRepository, roleAssignedToUserRepository, permissionAssignedToRoleRepository);
        }

        #region Métodos síncronos

        /// <inheritdoc />
        [OperationHandler]
        public Response<Role> AddRole (AddRole_Command command) =>
            Executor.ExecuteSynchronousOperation(_useCases.AddRole.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<List<Role>> GetRoles (GetRoles_Query query) =>
            GetEntities(new GetEntities_Query<Role>());

        /// <inheritdoc />
        [OperationHandler]
        public Response<Role> GetRoleByID (GetRoleByID_Query query) =>
            GetEntityByID(new GetEntityByID_Query<Role>(query.RoleID));

        /// <inheritdoc />
        [OperationHandler]
        public Response<List<Role>> GetRolesByUserID (GetRolesByUserID_Query query) =>
            Executor.ExecuteSynchronousOperation(_useCases.GetRolesByUserID.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<Role> UpdateRole (UpdateRole_Command command) =>
            Executor.ExecuteSynchronousOperation(_useCases.UpdateRole.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<bool> DeleteRoleByID (DeleteRoleByID_Command command) =>
            DeleteEntityByID(new DeleteEntityByID_Command<Role>(command.RoleID));

        /// <inheritdoc />
        [OperationHandler]
        public Response<PermissionAssignedToRole> AddPermissionToRole (AddPermissionToRole_Command command) =>
            Executor.ExecuteSynchronousOperation(_useCases.AddPermissionToRole.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<bool> RemovePermissionFromRole (RemovePermissionFromRole_Command command) =>
            Executor.ExecuteSynchronousOperation(_useCases.RemovePermissionFromRole.Handle, command, _detailedLog);

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Role>> AddRoleAsync (AddRole_Command command) =>
            Executor.ExecuteAsynchronousOperation(_useCases.AddRole.HandleAsync, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<Role>>> GetRolesAsync (GetRoles_Query query) =>
            GetEntitiesAsync(new GetEntities_Query<Role>());

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Role>> GetRoleByIDAsync (GetRoleByID_Query query) =>
            GetEntityByIDAsync(new GetEntityByID_Query<Role>(query.RoleID));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<Role>>> GetRolesByUserIDAsync (GetRolesByUserID_Query query) =>
            Executor.ExecuteAsynchronousOperation(_useCases.GetRolesByUserID.HandleAsync, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Role>> UpdateRoleAsync (UpdateRole_Command command) =>
            Executor.ExecuteAsynchronousOperation(_useCases.UpdateRole.HandleAsync, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> DeleteRoleByIDAsync (DeleteRoleByID_Command command) =>
            DeleteEntityByIDAsync(new DeleteEntityByID_Command<Role>(command.RoleID));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<PermissionAssignedToRole>> AddPermissionToRoleAsync (AddPermissionToRole_Command command) =>
            Executor.ExecuteAsynchronousOperation(_useCases.AddPermissionToRole.HandleAsync, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> RemovePermissionFromRoleAsync (RemovePermissionFromRole_Command command) =>
            Executor.ExecuteAsynchronousOperation(_useCases.RemovePermissionFromRole.HandleAsync, command, _detailedLog);

        #endregion

    }

}