using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.AddEntity;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations {

    /// <summary>
    /// Clase que centraliza y proporciona acceso a los casos de uso genéricos relacionados con la gestión de entidades.
    /// Implementa inicialización lazy para optimizar el rendimiento al evitar instancias innecesarias.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja esta clase. Debe implementar la interfaz «IGenericEntity».</typeparam>
    public class GenericEntities_OperationHandlers<EntityType> where EntityType : IGenericEntity {

        #region Queries (Consultas)

        /// <summary>
        /// Caso de uso para obtener una entidad específica por su identificador único.
        /// </summary>
        private Lazy<GetEntityByID_QueryHandler<EntityType>> _getEntityByID { get; }
        public GetEntityByID_QueryHandler<EntityType> GetEntityByID => _getEntityByID.Value;

        /// <summary>
        /// Caso de uso para obtener todas las entidades disponibles en el sistema.
        /// </summary>
        private Lazy<GetEntities_QueryHandler<EntityType>> _getEntities { get; }
        public GetEntities_QueryHandler<EntityType> GetEntities => _getEntities.Value;

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Caso de uso para agregar una nueva entidad al sistema.
        /// </summary>
        private Lazy<AddEntity_CommandHandler<EntityType>> _addEntity { get; }
        public AddEntity_CommandHandler<EntityType> AddEntity => _addEntity.Value;

        /// <summary>
        /// Caso de uso para actualizar los datos de una entidad existente.
        /// </summary>
        private Lazy<UpdateEntity_CommandHandler<EntityType>> _updateEntity { get; }
        public UpdateEntity_CommandHandler<EntityType> UpdateEntity => _updateEntity.Value;

        /// <summary>
        /// Caso de uso para eliminar una entidad específica por su identificador único.
        /// </summary>
        private Lazy<DeleteEntityByID_CommandHandler<EntityType>> _deleteEntityByID { get; }
        public DeleteEntityByID_CommandHandler<EntityType> DeleteEntityByID => _deleteEntityByID.Value;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que inicializa los inicializadores lazy para los casos de uso.
        /// </summary>
        /// <param name="genericRepository">El repositorio genérico utilizado por los casos de uso. Este proporciona acceso a las operaciones de persistencia.</param>
        public GenericEntities_OperationHandlers (IGenericRepository<EntityType> genericRepository) {
            _getEntityByID = new Lazy<GetEntityByID_QueryHandler<EntityType>>(() => new GetEntityByID_QueryHandler<EntityType>(genericRepository));
            _getEntities = new Lazy<GetEntities_QueryHandler<EntityType>>(() => new GetEntities_QueryHandler<EntityType>(genericRepository));
            _addEntity = new Lazy<AddEntity_CommandHandler<EntityType>>(() => new AddEntity_CommandHandler<EntityType>(genericRepository));
            _updateEntity = new Lazy<UpdateEntity_CommandHandler<EntityType>>(() => new UpdateEntity_CommandHandler<EntityType>(genericRepository));
            _deleteEntityByID = new Lazy<DeleteEntityByID_CommandHandler<EntityType>>(() => new DeleteEntityByID_CommandHandler<EntityType>(genericRepository));
        }

        #endregion

    }

}