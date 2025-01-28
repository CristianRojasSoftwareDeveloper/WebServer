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
    public class Role_EntityFrameworkRepository : Generic_EntityFrameworkRepository<Role>, IRoleRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public Role_EntityFrameworkRepository (ApplicationDbContext dbContext) : base(dbContext) { }

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<Role> AddRole (Role newRole) =>
            AddEntity(newRole);

        /// <inheritdoc />
        public Task<List<Role>> GetRoles (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <inheritdoc />
        public Task<Role?> GetRoleByID (int roleID, bool enableTracking = false) =>
            GetEntityByID(roleID, enableTracking);

        /// <inheritdoc />
        public Task<Role> UpdateRole (Partial<Role> roleUpdate) =>
            UpdateEntity(roleUpdate);

        /// <inheritdoc />
        public Task<bool> DeleteRoleByID (int roleID) =>
            DeleteEntityByID(roleID);

        #endregion

    }

}