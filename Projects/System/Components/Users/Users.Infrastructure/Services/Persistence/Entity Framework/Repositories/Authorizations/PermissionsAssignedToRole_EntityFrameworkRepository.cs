using Microsoft.EntityFrameworkCore;
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
    public class PermissionsAssignedToRole_EntityFrameworkRepository : Generic_EntityFrameworkRepository<PermissionAssignedToRole>, IPermissionsAssignedToRoleRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public PermissionsAssignedToRole_EntityFrameworkRepository (ApplicationDbContext dbContext) : base(dbContext) { }

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<PermissionAssignedToRole> AddPermissionAssignedToRole (PermissionAssignedToRole newPermissionAssignedToRole) =>
            AddEntity(newPermissionAssignedToRole);

        /// <inheritdoc />
        public Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRoles (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <inheritdoc />
        public Task<PermissionAssignedToRole?> GetPermissionAssignedToRoleByID (int permissionAssignedToRoleID, bool enableTracking = false) =>
            GetEntityByID(permissionAssignedToRoleID, enableTracking);

        public Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRolesByRoleID (int roleID, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(permissionAssignedToRole => permissionAssignedToRole.RoleID == roleID).
            Include(permissionAssignedToRole => permissionAssignedToRole.Permission).
            ToListAsync();

        public Task<PermissionAssignedToRole?> GetPermissionAssignedToRoleByForeignKeys (int roleID, int permissionID, bool enableTracking = false) =>
            FirstOrDefault(permissionAssignedToRole => permissionAssignedToRole.RoleID == roleID && permissionAssignedToRole.PermissionID == permissionID, enableTracking);

        /// <inheritdoc />
        public Task<PermissionAssignedToRole> UpdatePermissionAssignedToRole (Partial<PermissionAssignedToRole> permissionAssignedToRoleUpdate) =>
            UpdateEntity(permissionAssignedToRoleUpdate);

        /// <inheritdoc />
        public Task<bool> DeletePermissionAssignedToRoleByID (int permissionAssignedToRoleID) =>
            DeleteEntityByID(permissionAssignedToRoleID);

        #endregion

    }

}