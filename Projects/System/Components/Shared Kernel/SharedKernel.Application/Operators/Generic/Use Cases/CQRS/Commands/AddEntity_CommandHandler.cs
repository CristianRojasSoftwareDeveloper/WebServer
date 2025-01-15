using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de creación de una nueva entidad genérica.
    /// </summary>
    public class AddEntity_CommandHandler<EntityType> : ISyncOperationHandler<AddEntity_Command<EntityType>, EntityType>, IAsyncOperationHandler<AddEntity_Command<EntityType>, EntityType> where EntityType : IGenericEntity {

        private IGenericRepository<EntityType> _genericRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos AddEntity.
        /// </summary>
        /// <param name="genericRepository">El repositorio de tipo genérico.</param>
        public AddEntity_CommandHandler (IGenericRepository<EntityType> genericRepository) =>
            _genericRepository = genericRepository;

        /// <summary>
        /// Maneja el comando de creación de una nueva entidad de forma síncrona.
        /// </summary>
        /// <param name="command">El comando de creación de entidad.</param>
        /// <returns>La entidad creada.</returns>
        public EntityType Handle (AddEntity_Command<EntityType> command) =>
            _genericRepository.AddEntity(command.Entity, command.TrySetCreationDatetime, command.TrySetUpdateDatetime);

        /// <summary>
        /// Maneja el comando de creación de una nueva entidad de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de creación de entidad.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con la entidad creada.</returns>
        public Task<EntityType> HandleAsync (AddEntity_Command<EntityType> command) =>
            _genericRepository.AddEntityAsync(command.Entity, command.TrySetCreationDatetime, command.TrySetUpdateDatetime);

    }

}