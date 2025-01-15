using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de eliminación de una entidad por su ID.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad a eliminar.</typeparam>
    public class DeleteEntityByID_CommandHandler<EntityType> : ISyncOperationHandler<DeleteEntityByID_Command<EntityType>, bool>, IAsyncOperationHandler<DeleteEntityByID_Command<EntityType>, bool> where EntityType : IGenericEntity {

        private IGenericRepository<EntityType> _genericRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de eliminación de entidades por ID.
        /// </summary>
        /// <param name="genericRepository">El repositorio de tipo genérico que maneja las entidades.</param>
        public DeleteEntityByID_CommandHandler (IGenericRepository<EntityType> genericRepository) =>
            _genericRepository = genericRepository;

        /// <summary>
        /// Maneja el comando de eliminación de una entidad por su ID de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID de la entidad a eliminar.</param>
        /// <returns>Un booleano que indica si la entidad se eliminó correctamente (true) o no (false).</returns>
        public bool Handle (DeleteEntityByID_Command<EntityType> command) =>
            _genericRepository.DeleteEntityByID(command.EntityID);

        /// <summary>
        /// Maneja el comando de eliminación de una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID de la entidad a eliminar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un booleano que indica si la eliminación fue exitosa (true) o no (false).</returns>
        public Task<bool> HandleAsync (DeleteEntityByID_Command<EntityType> command) =>
            _genericRepository.DeleteEntityByIDAsync(command.EntityID);

    }

}