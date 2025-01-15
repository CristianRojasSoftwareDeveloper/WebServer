using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Domain.Models.Abstractions.Interfaces;
using System.Linq.Expressions;

namespace SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Repositories {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public abstract class Generic_EntityFrameworkRepository<EntityType> : IGenericRepository<EntityType> where EntityType : class, IGenericEntity {

        protected DbContext DbContext { get; }

        protected DbSet<EntityType> EntityRepository { get; }

        protected IQueryable<EntityType> GetQueryable () => EntityRepository;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio genérico.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        protected Generic_EntityFrameworkRepository (DbContext dbContext) {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            EntityRepository = DbContext.Set<EntityType>();
        }

        /// <summary>
        /// Mapea las propiedades de una entidad fuente a una entidad destino.
        /// </summary>
        /// <param name="sourceEntity">La entidad fuente que contiene los valores a copiar.</param>
        /// <param name="targetEntity">La entidad destino donde se copiarán los valores.</param>
        /// <param name="updatePatch">Indica si solo se deben actualizar las propiedades no nulas.</param>
        private void MapProperties (EntityType sourceEntity, EntityType targetEntity, bool updatePatch = true) {
            var entityTypeProperties = typeof(EntityType).GetProperties();
            foreach (var property in entityTypeProperties) {
                if (property.Name.Equals("ID")) // Ignora la propiedad ID durante la actualización.
                    continue;
                var sourcePropertyValue = property.GetValue(sourceEntity);
                if (!updatePatch || sourcePropertyValue != null) // Si el valor no es nulo o si se debe actualizar todo.
                    property.SetValue(targetEntity, sourcePropertyValue);
            }
            return;
        }

        protected int SaveChanges () => DbContext.SaveChanges();

        protected Task<int> SaveChangesAsync () => DbContext.SaveChangesAsync();

        #region Operaciones sincrónicas

        /// <summary>
        /// Agrega una nueva entidad al repositorio de forma sincrónica.
        /// </summary>
        /// <param name="newEntity">La entidad a agregar.</param>
        /// <param name="trySetCreationDatetime">Indica si se debe establecer la fecha de creación.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización.</param>
        /// <returns>La entidad agregada.</returns>
        public EntityType AddEntity (EntityType newEntity, bool trySetCreationDatetime = false, bool trySetUpdateDatetime = false) {
            ArgumentNullException.ThrowIfNull(newEntity);

            if (trySetCreationDatetime) {
                var createdAtProperty = typeof(EntityType).GetProperty("CreatedAt");
                // Establece la fecha de creación si la propiedad existe y su valor es nulo.
                if (createdAtProperty != null && createdAtProperty.GetValue(newEntity) == null)
                    createdAtProperty.SetValue(newEntity, DateTime.Now);
            }

            if (trySetUpdateDatetime) {
                var updatedAtProperty = typeof(EntityType).GetProperty("UpdatedAt");
                // Establece la fecha de actualización si la propiedad existe y su valor es nulo.
                if (updatedAtProperty != null && updatedAtProperty.GetValue(newEntity) == null)
                    updatedAtProperty.SetValue(newEntity, DateTime.Now);
            }

            EntityRepository.Add(newEntity);
            var hasChanged = SaveChanges() > 0;
            return hasChanged ? newEntity : default;
        }

        /// <summary>
        /// Obtiene todas las entidades del repositorio de forma sincrónica.
        /// </summary>
        /// <returns>Una lista de todas las entidades.</returns>
        public List<EntityType> GetEntities () {
            return EntityRepository.ToList();
        }

        /// <summary>
        /// Obtiene una entidad por su ID de forma sincrónica.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a buscar.</param>
        /// <returns>La entidad encontrada, o null si no se encuentra.</returns>
        public EntityType GetEntityByID (int entityID) {
            return EntityRepository.SingleOrDefault(entity => entity.ID == entityID);
        }

        public EntityType? FirstOrDefault (Expression<Func<EntityType, bool>> predicate) {
            return EntityRepository.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Actualiza una entidad en el repositorio de forma sincrónica.
        /// </summary>
        /// <param name="entityUpdate">La entidad con los valores actualizados.</param>
        /// <param name="updatePatch">Indica si solo se deben actualizar las propiedades no nulas.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización a la fecha actual.</param>
        /// <returns>La entidad actualizada.</returns>
        public EntityType UpdateEntity (EntityType entityUpdate, bool updatePatch = true, bool trySetUpdateDatetime = false) {
            ArgumentNullException.ThrowIfNull(entityUpdate);

            var entityToUpdate = EntityRepository.Find(entityUpdate.ID);
            if (entityToUpdate == null)
                throw NotFoundError.Create(typeof(EntityType).Name, entityUpdate.ID);

            MapProperties(entityUpdate, entityToUpdate, updatePatch);

            if (trySetUpdateDatetime) {
                var updatedAtProperty = typeof(EntityType).GetProperty("UpdatedAt");
                // Establece la fecha de actualización si la propiedad existe y su valor es nulo.
                if (updatedAtProperty != null && updatedAtProperty.GetValue(entityUpdate) == null)
                    updatedAtProperty.SetValue(entityToUpdate, DateTime.Now);
            }

            EntityRepository.Update(entityToUpdate);
            var hasChanged = SaveChanges() > 0;
            return hasChanged ? entityToUpdate : default;
        }

        /// <summary>
        /// Elimina una entidad del repositorio por su ID de forma sincrónica.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a eliminar.</param>
        /// <returns>True si la entidad se eliminó correctamente, False si no se encontró la entidad.</returns>
        public bool DeleteEntityByID (int entityID) {
            var entityToDelete = EntityRepository.Find(entityID);
            if (entityToDelete == null)
                throw NotFoundError.Create(typeof(EntityType).Name, entityID);

            EntityRepository.Remove(entityToDelete);
            var hasChanged = SaveChanges() > 0;
            return hasChanged;
        }

        #endregion

        #region Operaciones asíncronas

        /// <summary>
        /// Agrega de manera asíncrona una nueva entidad al repositorio.
        /// </summary>
        /// <param name="newEntity">La entidad a agregar.</param>
        /// <param name="trySetCreationDatetime">Indica si se debe establecer la fecha de creación.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización.</param>
        /// <returns>La entidad agregada.</returns>
        public async Task<EntityType> AddEntityAsync (EntityType newEntity, bool trySetCreationDatetime = false, bool trySetUpdateDatetime = false) {
            ArgumentNullException.ThrowIfNull(newEntity);

            if (trySetCreationDatetime) {
                var createdAtProperty = typeof(EntityType).GetProperty("CreatedAt");
                // Establece la fecha de creación si la propiedad existe y su valor es nulo.
                if (createdAtProperty != null && createdAtProperty.GetValue(newEntity) == null)
                    createdAtProperty.SetValue(newEntity, DateTime.Now);
            }

            if (trySetUpdateDatetime) {
                var updatedAtProperty = typeof(EntityType).GetProperty("UpdatedAt");
                // Establece la fecha de actualización si la propiedad existe y su valor es nulo.
                if (updatedAtProperty != null && updatedAtProperty.GetValue(newEntity) == null)
                    updatedAtProperty.SetValue(newEntity, DateTime.Now);
            }

            await EntityRepository.AddAsync(newEntity);
            var hasChanged = await SaveChangesAsync() > 0;
            return hasChanged ? newEntity : default;
        }

        /// <summary>
        /// Obtiene de manera asíncrona todas las entidades del repositorio.
        /// </summary>
        /// <returns>Una lista de todas las entidades.</returns>
        public Task<List<EntityType>> GetEntitiesAsync () {
            return EntityRepository.ToListAsync();
        }

        /// <summary>
        /// Obtiene de manera asíncrona una entidad por su ID.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a buscar.</param>
        /// <returns>La entidad encontrada, o null si no se encuentra.</returns>
        public async Task<EntityType> GetEntityByIDAsync (int entityID) {
            return await EntityRepository.SingleOrDefaultAsync(entity => entity.ID == entityID);
        }

        public Task<EntityType?> FirstOrDefaultAsync (Expression<Func<EntityType, bool>> predicate) {
            return EntityRepository.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Actualiza de manera asíncrona una entidad en el repositorio.
        /// </summary>
        /// <param name="entityUpdate">La entidad con los valores actualizados.</param>
        /// <param name="updatePatch">Indica si solo se deben actualizar las propiedades no nulas.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización a la fecha actual.</param>
        /// <returns>La entidad actualizada.</returns>
        public async Task<EntityType> UpdateEntityAsync (EntityType entityUpdate, bool updatePatch = true, bool trySetUpdateDatetime = false) {
            ArgumentNullException.ThrowIfNull(entityUpdate);

            var entityToUpdate = await EntityRepository.FindAsync(entityUpdate.ID);
            if (entityToUpdate == null)
                throw NotFoundError.Create(typeof(EntityType).Name, entityUpdate.ID);

            MapProperties(entityUpdate, entityToUpdate, updatePatch);

            if (trySetUpdateDatetime) {
                var updatedAtProperty = typeof(EntityType).GetProperty("UpdatedAt");
                // Establece la fecha de actualización si la propiedad existe y su valor es nulo.
                if (updatedAtProperty != null && updatedAtProperty.GetValue(entityUpdate) == null)
                    updatedAtProperty.SetValue(entityToUpdate, DateTime.Now);
            }

            EntityRepository.Update(entityToUpdate);
            var hasChanged = await SaveChangesAsync() > 0;
            return hasChanged ? entityToUpdate : default;
        }

        /// <summary>
        /// Elimina de manera asíncrona una entidad del repositorio por su ID.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a eliminar.</param>
        /// <returns>True si la entidad se eliminó correctamente, False si no se encontró la entidad.</returns>
        public async Task<bool> DeleteEntityByIDAsync (int entityID) {
            var entityToDelete = await EntityRepository.FindAsync(entityID);
            if (entityToDelete == null)
                throw NotFoundError.Create(typeof(EntityType).Name, entityID);

            EntityRepository.Remove(entityToDelete);
            var hasChanged = await SaveChangesAsync() > 0;
            return hasChanged;
        }

        #endregion

    }

}