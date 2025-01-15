using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Repositories;

namespace Users.Infrastructure.Services.Persistence.EntityFramework.Repositories.Authorizations {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class Role_EntityFrameworkRepository : Generic_EntityFrameworkRepository<Role>, IRoleRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public Role_EntityFrameworkRepository (DbContext dbContext) : base(dbContext) { }

        #region Métodos síncronos

        /// <inheritdoc />
        public Role AddRole (Role newRole) =>
            AddEntity(newRole, true, true);

        /// <inheritdoc />
        public List<Role> GetRoles () =>
            GetEntities();

        /// <inheritdoc />
        public Role GetRoleByID (int roleID) =>
            GetEntityByID(roleID);

        /// <inheritdoc />
        public Role UpdateRole (Role roleUpdate) =>
            UpdateEntity(roleUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public bool DeleteRoleByID (int roleID) =>
            DeleteEntityByID(roleID);

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<Role> AddRoleAsync (Role newRole) =>
            AddEntityAsync(newRole, true, true);

        /// <inheritdoc />
        public Task<List<Role>> GetRolesAsync () =>
            GetEntitiesAsync();

        /// <inheritdoc />
        public Task<Role> GetRoleByIDAsync (int roleID) =>
            GetEntityByIDAsync(roleID);

        /// <inheritdoc />
        public Task<Role> UpdateRoleAsync (Role roleUpdate) =>
            UpdateEntityAsync(roleUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public Task<bool> DeleteRoleByIDAsync (int roleID) =>
            DeleteEntityByIDAsync(roleID);

        #endregion

    }

}