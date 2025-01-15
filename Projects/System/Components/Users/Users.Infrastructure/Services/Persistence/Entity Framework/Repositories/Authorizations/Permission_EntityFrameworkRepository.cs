using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Repositories;

namespace Users.Infrastructure.Services.Persistence.EntityFramework.Repositories.Authorizations {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class Permission_EntityFrameworkRepository : Generic_EntityFrameworkRepository<Permission>, IPermissionRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public Permission_EntityFrameworkRepository (DbContext dbContext) : base(dbContext) { }

        #region Métodos síncronos

        /// <inheritdoc />
        public Permission AddPermission (Permission newPermission) =>
            AddEntity(newPermission, true, true);

        /// <inheritdoc />
        public List<Permission> GetPermissions () =>
            GetEntities();

        /// <inheritdoc />
        public Permission GetPermissionByID (int permissionID) =>
            GetEntityByID(permissionID);

        /// <inheritdoc />
        public Permission UpdatePermission (Permission permissionUpdate) =>
            UpdateEntity(permissionUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public bool DeletePermissionByID (int permissionID) =>
            DeleteEntityByID(permissionID);

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<Permission> AddPermissionAsync (Permission newPermission) =>
            AddEntityAsync(newPermission, true, true);

        /// <inheritdoc />
        public Task<List<Permission>> GetPermissionsAsync () =>
            GetEntitiesAsync();

        /// <inheritdoc />
        public Task<Permission> GetPermissionByIDAsync (int permissionID) =>
            GetEntityByIDAsync(permissionID);

        /// <inheritdoc />
        public Task<Permission> UpdatePermissionAsync (Permission permissionUpdate) =>
            UpdateEntityAsync(permissionUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public Task<bool> DeletePermissionByIDAsync (int permissionID) =>
            DeleteEntityByIDAsync(permissionID);

        #endregion

    }

}