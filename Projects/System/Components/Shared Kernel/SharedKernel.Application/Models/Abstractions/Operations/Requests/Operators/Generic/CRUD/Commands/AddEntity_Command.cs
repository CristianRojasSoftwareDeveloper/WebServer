using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands {

    /// <summary>
    /// Comando para agregar una nueva entidad.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad a agregar.</typeparam>
    [RequiredPermissions(Enumerations.Permissions.AddEntity)]
    public class AddEntity_Command<EntityType> : Operation where EntityType : IGenericEntity {

        /// <summary>
        /// Obtiene la entidad que se va a agregar.
        /// </summary>
        public EntityType Entity { get; }

        /// <summary>
        /// Obtiene un valor que indica si se debe establecer la fecha de creación.
        /// </summary>
        public bool TrySetCreationDatetime { get; }

        /// <summary>
        /// Obtiene un valor que indica si se debe establecer la fecha de actualización.
        /// </summary>
        public bool TrySetUpdateDatetime { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para agregar una entidad.
        /// </summary>
        /// <param name="entity">La entidad que se va a agregar.</param>
        /// <param name="trySetCreationDatetime">Indica si se debe establecer la fecha de creación.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización.</param>
        public AddEntity_Command (EntityType entity, bool trySetCreationDatetime = false, bool trySetUpdateDatetime = false) {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            TrySetCreationDatetime = trySetCreationDatetime;
            TrySetUpdateDatetime = trySetUpdateDatetime;
        }

    }

}