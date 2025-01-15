using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Operators.Generic.UseCases.CQRS.Commands;
using SharedKernel.Application.Operators.Generic.UseCases.CQRS.Queries;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.UseCases {

    /// <summary>
    /// Clase que proporciona acceso a todos los casos de uso relacionados con las tareas.
    /// </summary>
    public class Entities_UseCases<EntityType> where EntityType : IGenericEntity {

        public AddEntity_CommandHandler<EntityType> AddEntity { get; }
        public GetEntityByID_QueryHandler<EntityType> GetEntityByID { get; }
        public GetEntities_QueryHandler<EntityType> GetEntities { get; }
        public UpdateEntity_CommandHandler<EntityType> UpdateEntity { get; }
        public DeleteEntityByID_CommandHandler<EntityType> DeleteEntityByID { get; }

        /// <summary>
        /// Constructor que inicializa todos los casos de uso con el repositorio de tareas proporcionado.
        /// </summary>
        /// <param name="userRepository">El repositorio de tareas utilizado por los casos de uso.</param>
        public Entities_UseCases (IGenericRepository<EntityType> genericRepository) {
            AddEntity = new AddEntity_CommandHandler<EntityType>(genericRepository);
            GetEntityByID = new GetEntityByID_QueryHandler<EntityType>(genericRepository);
            GetEntities = new GetEntities_QueryHandler<EntityType>(genericRepository);
            UpdateEntity = new UpdateEntity_CommandHandler<EntityType>(genericRepository);
            DeleteEntityByID = new DeleteEntityByID_CommandHandler<EntityType>(genericRepository);
        }

    }

}