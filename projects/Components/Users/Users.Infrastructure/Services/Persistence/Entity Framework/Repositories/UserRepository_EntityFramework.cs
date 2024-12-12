using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework;
using System.Linq.Expressions;

namespace Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class UserRepository_EntityFramework : GenericRepository_EntityFramework<User>, IUserRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public UserRepository_EntityFramework (DbContext dbContext) : base(dbContext) { }

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
            EntityRepository.FirstOrDefault(u => u.Username == username);

        public User FirstUserOrDefault (Expression<Func<User, bool>> predicate) =>
            FirstOrDefault(predicate);

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
            FirstOrDefaultAsync(u => u.Username == username);

        public Task<User> FirstUserOrDefaultAsync (Expression<Func<User, bool>> predicate) =>
            FirstOrDefaultAsync(predicate);

        /// <inheritdoc />
        public Task<User> UpdateUserAsync (User userUpdate) =>
            UpdateEntityAsync(userUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public Task<bool> DeleteUserByIDAsync (int userID) =>
            DeleteEntityByIDAsync(userID);

        #endregion

    }

}