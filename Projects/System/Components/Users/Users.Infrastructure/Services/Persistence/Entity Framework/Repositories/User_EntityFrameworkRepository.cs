using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Repositories;

namespace Users.Infrastructure.Services.Persistence.EntityFramework.Repositories {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class User_EntityFrameworkRepository : Generic_EntityFrameworkRepository<User>, IUserRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public User_EntityFrameworkRepository (DbContext dbContext) : base(dbContext) { }

        #region Métodos síncronos

        /// <inheritdoc />
        public User AddUser (User newUser) =>
            AddEntity(newUser, true, true);

        /// <inheritdoc />
        public List<User> GetUsers () =>
            GetEntities();

        /// <inheritdoc />
        public User GetUserByID (int userID) =>
            GetEntityByID(userID);

        public User GetUserByUsername (string username) =>
            EntityRepository.
            Include(u => u.RolesAssignedToUser).
            ThenInclude(ur => ur.Role).
            ThenInclude(r => r.PermissionAssignedToRoles).
            ThenInclude(rp => rp.Permission).
            FirstOrDefault(u => u.Username == username);

        /// <inheritdoc />
        public User UpdateUser (User userUpdate) =>
            UpdateEntity(userUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public bool DeleteUserByID (int userID) =>
            DeleteEntityByID(userID);

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<User> AddUserAsync (User newUser) =>
            AddEntityAsync(newUser, true, true);

        /// <inheritdoc />
        public Task<List<User>> GetUsersAsync () =>
            GetEntitiesAsync();

        /// <inheritdoc />
        public Task<User> GetUserByIDAsync (int userID) =>
            GetEntityByIDAsync(userID);

        public Task<User> GetUserByUsernameAsync (string username) =>
            GetQueryable().
            Include(u => u.RolesAssignedToUser).
            ThenInclude(ur => ur.Role).
            ThenInclude(r => r.PermissionAssignedToRoles).
            ThenInclude(rp => rp.Permission).
            FirstOrDefaultAsync(u => u.Username == username);

        /// <inheritdoc />
        public Task<User> UpdateUserAsync (User userUpdate) =>
            UpdateEntityAsync(userUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public Task<bool> DeleteUserByIDAsync (int userID) =>
            DeleteEntityByIDAsync(userID);

        #endregion

    }

}