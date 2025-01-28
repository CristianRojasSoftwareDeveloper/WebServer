using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.UpdateEntity {

    /// <summary>
    /// Manejador para el comando de actualización de una entidad genérica.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que se actualizará.</typeparam>
    public class UpdateEntity_CommandHandler<EntityType> : IOperationHandler<IUpdateEntity_Command<EntityType>, EntityType> where EntityType : IGenericEntity {

        private IGenericRepository<EntityType> _genericRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de actualización de entidades.
        /// </summary>
        /// <param name="genericRepository">El repositorio genérico utilizado para acceder a los datos.</param>
        public UpdateEntity_CommandHandler (IGenericRepository<EntityType> genericRepository) =>
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));

        /// <summary>
        /// Maneja el comando de actualización de una entidad genérica de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de actualización de entidad.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con la entidad actualizada o null si no se encontró.</returns>
        public Task<EntityType> Handle (IUpdateEntity_Command<EntityType> command) =>
            _genericRepository.UpdateEntity(command.EntityUpdate);

    }

}