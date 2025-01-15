using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Repositories;

namespace Users.Infrastructure.Services.Persistence.EntityFramework.Repositories.Authorizations {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class RoleAssignedToUser_EntityFrameworkRepository : Generic_EntityFrameworkRepository<RoleAssignedToUser>, IRoleAssignedToUserRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public RoleAssignedToUser_EntityFrameworkRepository (DbContext dbContext) : base(dbContext) { }

        #region Métodos síncronos

        /// <inheritdoc />
        public RoleAssignedToUser AddRoleAssignedToUser (RoleAssignedToUser newRoleAssignedToUser) =>
            AddEntity(newRoleAssignedToUser, true, true);

        /// <inheritdoc />
        public RoleAssignedToUser GetRoleAssignedToUserByID (int roleAssignedToUserID) =>
            GetEntityByID(roleAssignedToUserID);

        public RoleAssignedToUser GetRoleAssignedToUserByForeignKeys (int userID, int roleID) =>
            GetQueryable().
            FirstOrDefault(roleAssignedToUser => roleAssignedToUser.UserID == userID && roleAssignedToUser.RoleID == roleID);

        /// <inheritdoc />
        public List<RoleAssignedToUser> GetRolesAssignedToUsers () =>
            GetEntities();

        public List<RoleAssignedToUser> GetRolesAssignedToUserByUserID (int userID) =>
            GetQueryable().
            Where(ur => ur.UserID == userID).
            Include(ur => ur.Role).
            ThenInclude(r => r.PermissionAssignedToRoles).
            ThenInclude(rp => rp.Permission).
            ToList();

        /// <inheritdoc />
        public RoleAssignedToUser UpdateRoleAssignedToUser (RoleAssignedToUser roleAssignedToUserUpdate) =>
            UpdateEntity(roleAssignedToUserUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public bool DeleteRoleAssignedToUserByID (int roleAssignedToUserID) =>
            DeleteEntityByID(roleAssignedToUserID);

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<RoleAssignedToUser> AddRoleAssignedToUserAsync (RoleAssignedToUser newRoleAssignedToUser) =>
            AddEntityAsync(newRoleAssignedToUser, true, true);

        /// <inheritdoc />
        public Task<RoleAssignedToUser> GetRoleAssignedToUserByIDAsync (int roleAssignedToUserID) =>
            GetEntityByIDAsync(roleAssignedToUserID);

        public Task<RoleAssignedToUser> GetRoleAssignedToUserByForeignKeysAsync (int userID, int roleID) =>
            GetQueryable().
            FirstOrDefaultAsync(roleAssignedToUser => roleAssignedToUser.UserID == userID && roleAssignedToUser.RoleID == roleID);

        /// <inheritdoc />
        public Task<List<RoleAssignedToUser>> GetRolesAssignedToUsersAsync () =>
            GetEntitiesAsync();

        public Task<List<RoleAssignedToUser>> GetRolesAssignedToUserByUserIDAsync (int userID) =>
            GetQueryable().
            Where(ur => ur.UserID == userID).
            Include(ur => ur.Role).
            ThenInclude(r => r.PermissionAssignedToRoles).
            ThenInclude(rp => rp.Permission).
            ToListAsync();

        /// <inheritdoc />
        public Task<RoleAssignedToUser> UpdateRoleAssignedToUserAsync (RoleAssignedToUser roleAssignedToUserUpdate) =>
            UpdateEntityAsync(roleAssignedToUserUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public Task<bool> DeleteRoleAssignedToUserByIDAsync (int roleAssignedToUserID) =>
            DeleteEntityByIDAsync(roleAssignedToUserID);

        #endregion

    }

}