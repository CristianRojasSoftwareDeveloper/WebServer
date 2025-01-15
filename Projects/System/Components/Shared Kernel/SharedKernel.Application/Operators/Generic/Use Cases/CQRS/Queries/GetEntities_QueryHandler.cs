using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.UseCases.CQRS.Queries {

    /// <summary>
    /// Manejador para la consulta de obtención de todas las entidades genéricas.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que se obtendrá.</typeparam>
    public class GetEntities_QueryHandler<EntityType> : ISyncOperationHandler<GetEntities_Query<EntityType>, List<EntityType>>, IAsyncOperationHandler<GetEntities_Query<EntityType>, List<EntityType>> where EntityType : IGenericEntity {

        private IGenericRepository<EntityType> _genericRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consultas de entidades.
        /// </summary>
        /// <param name="genericRepository">El repositorio genérico utilizado para acceder a los datos.</param>
        public GetEntities_QueryHandler (IGenericRepository<EntityType> genericRepository) =>
            _genericRepository = genericRepository;

        /// <summary>
        /// Maneja la consulta para obtener todas las entidades de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de entidades.</param>
        /// <returns>Una lista de entidades.</returns>
        public List<EntityType> Handle (GetEntities_Query<EntityType> query) =>
            _genericRepository.GetEntities();

        /// <summary>
        /// Maneja la consulta para obtener todas las entidades de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de entidades.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con una lista de entidades como resultado.</returns>
        public Task<List<EntityType>> HandleAsync (GetEntities_Query<EntityType> query) =>
            _genericRepository.GetEntitiesAsync();

    }

}