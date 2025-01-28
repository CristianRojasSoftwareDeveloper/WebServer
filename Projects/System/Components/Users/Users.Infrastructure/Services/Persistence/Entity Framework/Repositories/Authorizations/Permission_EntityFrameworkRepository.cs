using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories.Authorizations {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class Permission_EntityFrameworkRepository : Generic_EntityFrameworkRepository<Permission>, IPermissionRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public Permission_EntityFrameworkRepository (ApplicationDbContext dbContext) : base(dbContext) { }

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<Permission> AddPermission (Permission newPermission) =>
            AddEntity(newPermission);

        /// <inheritdoc />
        public Task<List<Permission>> GetPermissions (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <inheritdoc />
        public Task<Permission?> GetPermissionByID (int permissionID, bool enableTracking = false) =>
            GetEntityByID(permissionID, enableTracking);

        /// <inheritdoc />
        public Task<Permission> UpdatePermission (Partial<Permission> permissionUpdate) =>
            UpdateEntity(permissionUpdate);

        /// <inheritdoc />
        public Task<bool> DeletePermissionByID (int permissionID) =>
            DeleteEntityByID(permissionID);

        #endregion

    }

}