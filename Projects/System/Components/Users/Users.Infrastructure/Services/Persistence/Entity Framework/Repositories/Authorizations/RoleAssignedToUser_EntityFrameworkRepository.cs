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
    public class RoleAssignedToUser_EntityFrameworkRepository : Generic_EntityFrameworkRepository<RoleAssignedToUser>, IRoleAssignedToUserRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public RoleAssignedToUser_EntityFrameworkRepository (ApplicationDbContext dbContext) : base(dbContext) { }

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<RoleAssignedToUser> AddRoleAssignedToUser (RoleAssignedToUser newRoleAssignedToUser) =>
            AddEntity(newRoleAssignedToUser);

        /// <inheritdoc />
        public Task<List<RoleAssignedToUser>> GetRolesAssignedToUsers (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <inheritdoc />
        public Task<RoleAssignedToUser?> GetRoleAssignedToUserByID (int roleAssignedToUserID, bool enableTracking = false) =>
            GetEntityByID(roleAssignedToUserID, enableTracking);

        public Task<List<RoleAssignedToUser>> GetRolesAssignedToUserByUserID (int userID, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(roleAssignedToUser => roleAssignedToUser.UserID == userID).
            Include(roleAssignedToUser => roleAssignedToUser.Role).
            ThenInclude(role => role.PermissionAssignedToRoles).
            ThenInclude(permissionAssignedToRole => permissionAssignedToRole.Permission).
            ToListAsync();

        public Task<RoleAssignedToUser?> GetRoleAssignedToUserByForeignKeys (int userID, int roleID, bool enableTracking = false) =>
            FirstOrDefault(roleAssignedToUser => roleAssignedToUser.UserID == userID && roleAssignedToUser.RoleID == roleID, enableTracking);

        /// <inheritdoc />
        public Task<RoleAssignedToUser> UpdateRoleAssignedToUser (Partial<RoleAssignedToUser> roleAssignedToUserUpdate) =>
            UpdateEntity(roleAssignedToUserUpdate);

        /// <inheritdoc />
        public Task<bool> DeleteRoleAssignedToUserByID (int roleAssignedToUserID) =>
            DeleteEntityByID(roleAssignedToUserID);

        #endregion

    }

}