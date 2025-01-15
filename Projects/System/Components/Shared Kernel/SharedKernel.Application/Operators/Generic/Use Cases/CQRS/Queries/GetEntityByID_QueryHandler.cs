using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.UseCases.CQRS.Queries {

    /// <summary>
    /// Manejador para la consulta de obtención de una entidad genérica por su ID.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que se obtendrá.</typeparam>
    public class GetEntityByID_QueryHandler<EntityType> : ISyncOperationHandler<GetEntityByID_Query<EntityType>, EntityType>, IAsyncOperationHandler<GetEntityByID_Query<EntityType>, EntityType> where EntityType : IGenericEntity {

        private IGenericRepository<EntityType> _genericRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consultas de entidades por ID.
        /// </summary>
        /// <param name="genericRepository">El repositorio genérico utilizado para acceder a los datos.</param>
        public GetEntityByID_QueryHandler (IGenericRepository<EntityType> genericRepository) =>
            _genericRepository = genericRepository;

        /// <summary>
        /// Maneja la consulta para obtener una entidad por su ID de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de entidad por ID.</param>
        /// <returns>La entidad obtenida o null si no se encuentra.</returns>
        public EntityType Handle (GetEntityByID_Query<EntityType> query) =>
            _genericRepository.GetEntityByID(query.EntityID);

        /// <summary>
        /// Maneja la consulta para obtener una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de entidad por ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con la entidad obtenida o null si no se encuentra.</returns>
        public Task<EntityType> HandleAsync (GetEntityByID_Query<EntityType> query) =>
            _genericRepository.GetEntityByIDAsync(query.EntityID);

    }

}