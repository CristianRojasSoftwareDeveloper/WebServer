using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.AddEntity {

    /// <summary>
    /// Manejador para el comando de creación de una nueva entidad genérica.
    /// </summary>
    public class AddEntity_CommandHandler<EntityType> : IOperationHandler<IAddEntity_Command<EntityType>, EntityType> where EntityType : IGenericEntity {

        private IGenericRepository<EntityType> _genericRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos AddEntity.
        /// </summary>
        /// <param name="genericRepository">El repositorio de tipo genérico.</param>
        public AddEntity_CommandHandler (IGenericRepository<EntityType> genericRepository) =>
            _genericRepository = genericRepository;

        /// <summary>
        /// Maneja el comando de creación de una nueva entidad de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de creación de entidad.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con la entidad creada.</returns>
        public Task<EntityType> Handle (IAddEntity_Command<EntityType> command) =>
            _genericRepository.AddEntity(command.Entity);

    }

}