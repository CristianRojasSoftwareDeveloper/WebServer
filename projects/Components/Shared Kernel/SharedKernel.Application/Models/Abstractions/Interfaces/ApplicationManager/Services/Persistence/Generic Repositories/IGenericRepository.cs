using SharedKernel.Domain.Models.Abstractions.Interfaces;
using System.Linq.Expressions;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    /// <summary>
    /// Define las operaciones CRUD genéricas para un repositorio.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public interface IGenericRepository<EntityType> where EntityType : IGenericEntity {

        #region Métodos síncronos

        /// <summary>
        /// Agrega una nueva entidad al repositorio de forma sincrónica.
        /// </summary>
        /// <param name="newEntity">La entidad a agregar.</param>
        /// <param name="trySetCreationDatetime">Indica si se debe establecer la fecha de creación.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización.</param>
        /// <returns>La entidad agregada.</returns>
        EntityType AddEntity (EntityType newEntity, bool trySetCreationDatetime = false, bool trySetUpdateDatetime = false);

        /// <summary>
        /// Obtiene todas las entidades del repositorio.
        /// </summary>
        /// <returns>Una colección de entidades.</returns>
        List<EntityType> GetEntities ();

        /// <summary>
        /// Obtiene una entidad por su ID.
        /// </summary>
        /// <param name="entityID">El ID de la entidad.</param>
        /// <returns>La entidad encontrada o null si no existe.</returns>
        EntityType GetEntityByID (int entityID);

        /// <summary>
        /// Obtiene la primera entidad que cumpla con una condición determinada o el valor por defecto de forma síncrona.
        /// </summary>
        EntityType? FirstOrDefault (Expression<Func<EntityType, bool>> predicate);

        /// <summary>
        /// Actualiza una entidad en el repositorio de forma sincrónica.
        /// </summary>
        /// <param name="entityUpdate">La entidad con los valores actualizados.</param>
        /// <param name="updatePatch">Indica si solo se deben actualizar las propiedades no nulas.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización a la fecha actual.</param>
        /// <returns>La entidad actualizada.</returns>
        EntityType UpdateEntity (EntityType entityUpdate, bool updatePatch = true, bool trySetUpdateDatetime = false);

        /// <summary>
        /// Elimina una entidad por su ID.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, false si no se encontró la entidad.</returns>
        bool DeleteEntityByID (int entityID);

        #endregion

        #region Métodos asíncronos

        /// <summary>
        /// Agrega de manera asíncrona una nueva entidad al repositorio.
        /// </summary>
        /// <param name="newEntity">La entidad a agregar.</param>
        /// <param name="trySetCreationDatetime">Indica si se debe establecer la fecha de creación.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización.</param>
        /// <returns>La entidad agregada.</returns>
        Task<EntityType> AddEntityAsync (EntityType newEntity, bool trySetCreationDatetime = false, bool trySetUpdateDatetime = false);

        /// <summary>
        /// Obtiene todas las entidades del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de entidades como resultado.</returns>
        Task<List<EntityType>> GetEntitiesAsync ();

        /// <summary>
        /// Obtiene una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="entityID">El ID de la entidad.</param>
        /// <returns>La tarea que representa la operación asincrónica, con la entidad encontrada o null si no existe como resultado.</returns>
        Task<EntityType> GetEntityByIDAsync (int entityID);

        /// <summary>
        /// Obtiene la primera entidad que cumpla con una condición determinada o el valor por defecto de forma asíncrona.
        /// </summary>
        Task<EntityType?> FirstOrDefaultAsync (Expression<Func<EntityType, bool>> predicate);

        /// <summary>
        /// Actualiza de manera asíncrona una entidad en el repositorio.
        /// </summary>
        /// <param name="entityUpdate">La entidad con los valores actualizados.</param>
        /// <param name="updatePatch">Indica si solo se deben actualizar las propiedades no nulas.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización a la fecha actual.</param>
        /// <returns>La entidad actualizada.</returns>
        Task<EntityType> UpdateEntityAsync (EntityType entityUpdate, bool updatePatch = true, bool trySetUpdateDatetime = false);

        /// <summary>
        /// Elimina una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<bool> DeleteEntityByIDAsync (int entityID);

        #endregion

    }

}