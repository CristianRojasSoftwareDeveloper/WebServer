using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.UseCases.Queries;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using Users.Application.Operators.Permissions.UseCases;

namespace Users.Application.Operators.Permissions {

    /// <summary>
    /// Implementación específica de la interfaz <see cref="IPermissionOperator"/> para operaciones relacionadas con la gestión de los permisos de usuario.
    /// </summary>
    public class PermissionOperator : GenericOperator<Permission>, IPermissionOperator {

        /// <summary>
        /// Casos de uso específicos para permisos.
        /// </summary>
        private Permissions_UseCases _useCases { get; }

        /// <summary>
        /// Constructor que inicializa el operador de permiso de usuario con un repositorio específico de permisos de usuario y roles de permiso.
        /// </summary>
        /// <param name="permissionRepository">El repositorio específico de permisos de usuario.</param>
        /// <param name="permissionAssignedToRoleRepository">El repositorio específico de roles de permiso.</param>
        /// <param name="detailedLog">Indica si se debe utilizar un registro detallado.</param>
        public PermissionOperator (IPermissionRepository permissionRepository, IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository, bool detailedLog = false) : base((IGenericRepository<Permission>) permissionRepository, detailedLog)
            => _useCases = new Permissions_UseCases(permissionRepository, permissionAssignedToRoleRepository);

        #region Métodos síncronos

        /// <inheritdoc />
        [OperationHandler]
        public Response<Permission> AddPermission (AddPermission_Command command)
            => Executor.ExecuteSynchronousOperation(_useCases.AddPermission.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<List<Permission>> GetPermissions (GetPermissions_Query query)
            => GetEntities(new GetEntities_Query<Permission>());

        /// <inheritdoc />
        [OperationHandler]
        public Response<Permission> GetPermissionByID (GetPermissionByID_Query query)
            => GetEntityByID(new GetEntityByID_Query<Permission>(query.PermissionID));

        /// <inheritdoc />
        [OperationHandler]
        public Response<List<Permission>> GetPermissionsByRoleID (GetPermissionsByRoleID_Query query)
            => Executor.ExecuteSynchronousOperation(_useCases.GetPermissionsByRoleID.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<Permission> UpdatePermission (UpdatePermission_Command command)
            => Executor.ExecuteSynchronousOperation(_useCases.UpdatePermission.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<bool> DeletePermissionByID (DeletePermissionByID_Command command)
            => DeleteEntityByID(new DeleteEntityByID_Command<Permission>(command.PermissionID));

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Permission>> AddPermissionAsync (AddPermission_Command command)
            => Executor.ExecuteAsynchronousOperation(_useCases.AddPermission.HandleAsync, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<Permission>>> GetPermissionsAsync (GetPermissions_Query query)
            => GetEntitiesAsync(new GetEntities_Query<Permission>());

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Permission>> GetPermissionByIDAsync (GetPermissionByID_Query query)
            => GetEntityByIDAsync(new GetEntityByID_Query<Permission>(query.PermissionID));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<Permission>>> GetPermissionsByRoleIDAsync (GetPermissionsByRoleID_Query query)
            => Executor.ExecuteAsynchronousOperation(_useCases.GetPermissionsByRoleID.HandleAsync, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<Permission>> UpdatePermissionAsync (UpdatePermission_Command command)
            => Executor.ExecuteAsynchronousOperation(_useCases.UpdatePermission.HandleAsync, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> DeletePermissionByIDAsync (DeletePermissionByID_Command command)
            => DeleteEntityByIDAsync(new DeleteEntityByID_Command<Permission>(command.PermissionID));

        #endregion

    }

}