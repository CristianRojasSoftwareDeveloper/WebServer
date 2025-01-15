using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Repositories;

namespace Users.Infrastructure.Services.Persistence.EntityFramework.Repositories.Authorizations {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class PermissionsAssignedToRole_EntityFrameworkRepository : Generic_EntityFrameworkRepository<PermissionAssignedToRole>, IPermissionsAssignedToRoleRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public PermissionsAssignedToRole_EntityFrameworkRepository (DbContext dbContext) : base(dbContext) { }

        #region Métodos síncronos

        /// <inheritdoc />
        public PermissionAssignedToRole AddPermissionAssignedToRole (PermissionAssignedToRole newPermissionAssignedToRole) =>
            AddEntity(newPermissionAssignedToRole, true, true);

        /// <inheritdoc />
        public List<PermissionAssignedToRole> GetPermissionAssignedToRoles () =>
            GetEntities();

        public List<PermissionAssignedToRole> GetPermissionAssignedToRolesByRoleID (int roleID) =>
            GetQueryable().
            Where(rp => rp.RoleID == roleID).
            Include(rp => rp.Permission).
            ToList();

        /// <inheritdoc />
        public PermissionAssignedToRole GetPermissionAssignedToRoleByID (int permissionAssignedToRoleID) =>
            GetEntityByID(permissionAssignedToRoleID);

        public PermissionAssignedToRole GetPermissionAssignedToRoleByForeignKeys (int roleID, int permissionID) =>
            GetQueryable().
            FirstOrDefault(permissionAssignedToRole => permissionAssignedToRole.RoleID == roleID && permissionAssignedToRole.PermissionID == permissionID);

        /// <inheritdoc />
        public PermissionAssignedToRole UpdatePermissionAssignedToRole (PermissionAssignedToRole permissionAssignedToRoleUpdate) =>
            UpdateEntity(permissionAssignedToRoleUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public bool DeletePermissionAssignedToRoleByID (int permissionAssignedToRoleID) =>
            DeleteEntityByID(permissionAssignedToRoleID);

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<PermissionAssignedToRole> AddPermissionAssignedToRoleAsync (PermissionAssignedToRole newPermissionAssignedToRole) =>
            AddEntityAsync(newPermissionAssignedToRole, true, true);

        /// <inheritdoc />
        public Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRolesAsync () =>
            GetEntitiesAsync();

        public Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRolesByRoleIDAsync (int roleID) =>
            GetQueryable().
            Where(rp => rp.RoleID == roleID).
            Include(rp => rp.Permission).
            ToListAsync();

        /// <inheritdoc />
        public Task<PermissionAssignedToRole> GetPermissionAssignedToRoleByIDAsync (int permissionAssignedToRoleID) =>
            GetEntityByIDAsync(permissionAssignedToRoleID);

        public Task<PermissionAssignedToRole> GetPermissionAssignedToRoleByForeignKeysAsync (int roleID, int permissionID) =>
            GetQueryable().
            FirstOrDefaultAsync(permissionAssignedToRole => permissionAssignedToRole.RoleID == roleID && permissionAssignedToRole.PermissionID == permissionID);

        /// <inheritdoc />
        public Task<PermissionAssignedToRole> UpdatePermissionAssignedToRoleAsync (PermissionAssignedToRole permissionAssignedToRoleUpdate) =>
            UpdateEntityAsync(permissionAssignedToRoleUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public Task<bool> DeletePermissionAssignedToRoleByIDAsync (int permissionAssignedToRoleID) =>
            DeleteEntityByIDAsync(permissionAssignedToRoleID);

        #endregion

    }

}