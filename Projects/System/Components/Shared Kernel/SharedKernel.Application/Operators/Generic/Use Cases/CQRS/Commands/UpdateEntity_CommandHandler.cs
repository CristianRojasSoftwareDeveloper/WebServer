using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de actualización de una entidad genérica.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que se actualizará.</typeparam>
    public class UpdateEntity_CommandHandler<EntityType> : ISyncOperationHandler<UpdateEntity_Command<EntityType>, EntityType>, IAsyncOperationHandler<UpdateEntity_Command<EntityType>, EntityType> where EntityType : IGenericEntity {

        private IGenericRepository<EntityType> _genericRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de actualización de entidades.
        /// </summary>
        /// <param name="genericRepository">El repositorio genérico utilizado para acceder a los datos.</param>
        public UpdateEntity_CommandHandler (IGenericRepository<EntityType> genericRepository) =>
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));

        /// <summary>
        /// Maneja el comando de actualización de una entidad genérica de forma síncrona.
        /// </summary>
        /// <param name="entityUpdate">El comando de actualización de entidad.</param>
        /// <returns>La entidad actualizada o null si no se encontró.</returns>
        public EntityType Handle (UpdateEntity_Command<EntityType> entityUpdate) =>
            _genericRepository.UpdateEntity(entityUpdate.Entity, trySetUpdateDatetime: entityUpdate.TrySetUpdateDatetime);

        /// <summary>
        /// Maneja el comando de actualización de una entidad genérica de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de actualización de entidad.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con la entidad actualizada o null si no se encontró.</returns>
        public Task<EntityType> HandleAsync (UpdateEntity_Command<EntityType> entityUpdate) =>
            _genericRepository.UpdateEntityAsync(entityUpdate.Entity, trySetUpdateDatetime: entityUpdate.TrySetUpdateDatetime);

    }

}