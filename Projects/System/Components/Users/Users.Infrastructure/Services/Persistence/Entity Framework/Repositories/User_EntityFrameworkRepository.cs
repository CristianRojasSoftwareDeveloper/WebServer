using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class User_EntityFrameworkRepository : Generic_EntityFrameworkRepository<User>, IUserRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public User_EntityFrameworkRepository (ApplicationDbContext dbContext) : base(dbContext) { }

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<User> AddUser (User newUser) =>
            AddEntity(newUser);

        /// <inheritdoc />
        public Task<List<User>> GetUsers (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <inheritdoc />
        public Task<User?> GetUserByID (int userID, bool enableTracking = false) =>
            GetEntityByID(userID, enableTracking);

        public Task<User?> GetUserByUsername (string username, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(user => user.Username == username).
            Include(user => user.RolesAssignedToUser).
            ThenInclude(roleAssignedToUser => roleAssignedToUser.Role).
            ThenInclude(role => role.PermissionAssignedToRoles).
            ThenInclude(permissionAssignedToRole => permissionAssignedToRole.Permission).
            SingleOrDefaultAsync();

        /// <inheritdoc />
        public Task<User> UpdateUser (Partial<User> userUpdate) =>
            UpdateEntity(userUpdate);

        /// <inheritdoc />
        public Task<bool> DeleteUserByID (int userID) =>
            DeleteEntityByID(userID);

        #endregion

    }

}