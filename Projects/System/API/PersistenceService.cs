using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SystemLogs.Infrastructure.Services.Persistence.EntityFramework.Repositories;
using Users.Infrastructure.Services.Persistence.EntityFramework.Repositories;
using Users.Infrastructure.Services.Persistence.EntityFramework.Repositories.Authorizations;

namespace API {

    /// <summary>
    /// Implementación concreta de IPersistenceService.
    /// </summary>
    public class PersistenceService : IPersistenceService {

        /// <inheritdoc />
        public IUserRepository UserRepository { get; }

        /// <inheritdoc />
        public IRoleRepository RoleRepository { get; }

        /// <inheritdoc />
        public IPermissionRepository PermissionRepository { get; }

        /// <inheritdoc />
        public IRoleAssignedToUserRepository RoleAssignedToUserRepository { get; }

        /// <inheritdoc />
        public IPermissionsAssignedToRoleRepository PermissionAssignedToRoleRepository { get; }

        /// <inheritdoc />
        public ISystemLogRepository SystemLogRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia de PersistenceService.
        /// </summary>
        /// <param name="databaseInstance">Repositorio de usuarios.</param>
        public PersistenceService (DbContext databaseInstance) {
            ArgumentNullException.ThrowIfNull(databaseInstance, nameof(databaseInstance));
            databaseInstance.Database.EnsureCreated();
            // Inicializa los servicios de persistencia (Repositorios)
            UserRepository = new User_EntityFrameworkRepository(databaseInstance);
            RoleRepository = new Role_EntityFrameworkRepository(databaseInstance);
            PermissionRepository = new Permission_EntityFrameworkRepository(databaseInstance);
            RoleAssignedToUserRepository = new RoleAssignedToUser_EntityFrameworkRepository(databaseInstance);
            PermissionAssignedToRoleRepository = new PermissionsAssignedToRole_EntityFrameworkRepository(databaseInstance);
            SystemLogRepository = new SystemLog_EntityFrameworkRepository(databaseInstance);
        }

    }

}