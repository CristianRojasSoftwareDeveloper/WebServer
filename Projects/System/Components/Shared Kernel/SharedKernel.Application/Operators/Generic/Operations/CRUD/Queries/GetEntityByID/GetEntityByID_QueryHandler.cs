using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntityByID {

    /// <summary>
    /// Manejador para la consulta de obtención de una entidad genérica por su ID.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que se obtendrá.</typeparam>
    public class GetEntityByID_QueryHandler<EntityType> : IOperationHandler<IGetEntityByID_Query, EntityType> where EntityType : IGenericEntity {

        private IGenericRepository<EntityType> _genericRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consultas de entidades por ID.
        /// </summary>
        /// <param name="genericRepository">El repositorio genérico utilizado para acceder a los datos.</param>
        public GetEntityByID_QueryHandler (IGenericRepository<EntityType> genericRepository) =>
            _genericRepository = genericRepository;

        /// <summary>
        /// Maneja la consulta para obtener una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de entidad por ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con la entidad obtenida o null si no se encuentra.</returns>
        public Task<EntityType> Handle (IGetEntityByID_Query query) =>
            _genericRepository.GetEntityByID(query.ID, query.EnableTracking);

    }

}