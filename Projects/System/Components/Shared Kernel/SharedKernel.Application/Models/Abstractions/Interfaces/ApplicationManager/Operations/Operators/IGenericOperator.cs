using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators {

    /// <summary>
    /// Interfaz para operaciones compartidas por operadores y entidades genéricas.
    /// </summary>
    /// <typeparam name="EntityType">Tipo de entidad.</typeparam>
    public interface IGenericOperator<EntityType> : IReflexiveOperator where EntityType : IGenericEntity {

        #region Métodos síncronos

        /// <summary>
        /// Agrega una entidad de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene la nueva entidad a agregar.</param>
        /// <returns>Un resultado que contiene la entidad agregada o un Error.</returns>
        Response<EntityType> AddEntity (AddEntity_Command<EntityType> command);

        /// <summary>
        /// Obtiene todas las entidades de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta que define los criterios de búsqueda.</param>
        /// <returns>Un resultado que contiene las entidades encontradas o un Error.</returns>
        Response<List<EntityType>> GetEntities (GetEntities_Query<EntityType> query);

        /// <summary>
        /// Obtiene una entidad por su ID de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID de la entidad a buscar.</param>
        /// <returns>Un resultado que contiene la entidad encontrada o un Error.</returns>
        Response<EntityType> GetEntityByID (GetEntityByID_Query<EntityType> query);

        /// <summary>
        /// Actualiza una entidad de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene la entidad a actualizar.</param>
        /// <returns>Un resultado que contiene la entidad actualizada o un Error.</returns>
        Response<EntityType> UpdateEntity (UpdateEntity_Command<EntityType> command);

        /// <summary>
        /// Elimina una entidad de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID de la entidad a eliminar.</param>
        /// <returns>Un resultado que indica si la entidad fue eliminada o un Error.</returns>
        Response<bool> DeleteEntityByID (DeleteEntityByID_Command<EntityType> command);

        #endregion

        #region Métodos asíncronos

        /// <summary>
        /// Agrega una entidad de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene la nueva entidad a agregar.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene la entidad agregada o un Error.</returns>
        Task<Response<EntityType>> AddEntityAsync (AddEntity_Command<EntityType> command);

        /// <summary>
        /// Obtiene todas las entidades de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta que define los criterios de búsqueda.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene las entidades encontradas o un Error.</returns>
        Task<Response<List<EntityType>>> GetEntitiesAsync (GetEntities_Query<EntityType> query);

        /// <summary>
        /// Obtiene una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID de la entidad a buscar.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene la entidad encontrada o un Error.</returns>
        Task<Response<EntityType>> GetEntityByIDAsync (GetEntityByID_Query<EntityType> query);

        /// <summary>
        /// Actualiza una entidad de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene la entidad a actualizar.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene la entidad actualizada o un Error.</returns>
        Task<Response<EntityType>> UpdateEntityAsync (UpdateEntity_Command<EntityType> command);

        /// <summary>
        /// Elimina una entidad de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID de la entidad a eliminar.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene un resultado que indica si la entidad fue eliminada o un Error.</returns>
        Task<Response<bool>> DeleteEntityByIDAsync (DeleteEntityByID_Command<EntityType> command);

        #endregion

    }

}
