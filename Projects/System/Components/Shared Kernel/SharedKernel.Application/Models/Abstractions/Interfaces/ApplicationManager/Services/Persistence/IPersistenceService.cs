using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence {

    /// <summary>
    /// Interface que define los servicios de persistencia disponibles.
    /// </summary>
    public interface IPersistenceService {

        /// <summary>
        /// Repositorio de usuarios.
        /// </summary>
        IUserRepository UserRepository { get; }

        /// <summary>
        /// Repositorio de roles.
        /// </summary>
        IRoleRepository RoleRepository { get; }

        /// <summary>
        /// Repositorio de permisos.
        /// </summary>
        IPermissionRepository PermissionRepository { get; }

        /// <summary>
        /// Repositorio de roles de usuario.
        /// </summary>
        IRoleAssignedToUserRepository RoleAssignedToUserRepository { get; }

        /// <summary>
        /// Repositorio de permisos de roles.
        /// </summary>
        IPermissionsAssignedToRoleRepository PermissionAssignedToRoleRepository { get; }

        /// <summary>
        /// Repositorio de registros del sistema.
        /// </summary>
        ISystemLogRepository SystemLogRepository { get; }

    }

}