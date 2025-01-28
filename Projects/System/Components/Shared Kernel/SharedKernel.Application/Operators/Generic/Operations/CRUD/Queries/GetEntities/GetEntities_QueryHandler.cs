using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntities {

    /// <summary>
    /// Manejador para la consulta de obtención de todas las entidades genéricas.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que se obtendrá.</typeparam>
    public class GetEntities_QueryHandler<EntityType> : IOperationHandler<IGetEntities_Query, List<EntityType>> where EntityType : IGenericEntity {

        private IGenericRepository<EntityType> _genericRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consultas de entidades.
        /// </summary>
        /// <param name="genericRepository">El repositorio genérico utilizado para acceder a los datos.</param>
        public GetEntities_QueryHandler (IGenericRepository<EntityType> genericRepository) =>
            _genericRepository = genericRepository;

        /// <summary>
        /// Maneja la consulta para obtener todas las entidades de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de entidades.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con una lista de entidades como resultado.</returns>
        public Task<List<EntityType>> Handle (IGetEntities_Query query) =>
            _genericRepository.GetEntities(query.EnableTracking);

    }

}