using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SystemLogs.Infrastructure.Services.Persistence.Entity_Framework.Repositories;
using Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories;
using Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories.Authorizations;

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
        public PersistenceService (ApplicationDbContext databaseInstance) {
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