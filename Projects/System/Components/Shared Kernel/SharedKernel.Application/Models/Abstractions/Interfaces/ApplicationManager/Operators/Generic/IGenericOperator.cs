using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic {

    /// <summary>
    /// Interfaz para operaciones compartidas por operadores y entidades genéricas.
    /// </summary>
    /// <typeparam name="EntityType">Tipo de entidad.</typeparam>
    public interface IGenericOperator<EntityType> : IReflexiveOperator where EntityType : IGenericEntity {

        #region Operaciones CRUD asíncronas: Add, Get, Update, Delete

        /// <summary>
        /// Agrega una nueva entidad al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando que contiene la entidad a agregar.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta con la entidad agregada.</returns>
        Task<Response<EntityType>> AddEntity (IAddEntity_Command<EntityType> command);

        /// <summary>
        /// Obtiene todas las entidades del repositorio de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener las entidades.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta con la lista de entidades.</returns>
        Task<Response<List<EntityType>>> GetEntities (IGetEntities_Query query);

        /// <summary>
        /// Obtiene una entidad específica por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta que contiene el ID de la entidad a buscar.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta con la entidad encontrada.</returns>
        Task<Response<EntityType>> GetEntityByID (IGetEntityByID_Query query);

        /// <summary>
        /// Actualiza una entidad existente en el repositorio de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando que contiene la entidad actualizada.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta con la entidad actualizada.</returns>
        Task<Response<EntityType>> UpdateEntity (IUpdateEntity_Command<EntityType> command);

        /// <summary>
        /// Elimina una entidad del repositorio de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando que contiene el ID de la entidad a eliminar.</param>
        /// <returns>Tarea que representa la operación asíncrona y contiene la respuesta que indica el resultado de la operación.</returns>
        Task<Response<bool>> DeleteEntityByID (IDeleteEntityByID_Command command);

        #endregion

    }

}
