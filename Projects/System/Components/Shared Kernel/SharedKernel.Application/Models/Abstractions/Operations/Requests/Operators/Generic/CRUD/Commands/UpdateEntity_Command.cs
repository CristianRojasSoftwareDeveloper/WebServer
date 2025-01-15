using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands {

    /// <summary>
    /// Comando para actualizar una entidad existente.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad a actualizar.</typeparam>
    [RequiredPermissions(Enumerations.Permissions.UpdateEntity)]
    public class UpdateEntity_Command<EntityType> : Operation where EntityType : IGenericEntity {

        /// <summary>
        /// Obtiene la entidad que se va a actualizar.
        /// </summary>
        public EntityType Entity { get; }

        /// <summary>
        /// Obtiene un valor que indica si se debe establecer la fecha de actualización.
        /// </summary>
        public bool TrySetUpdateDatetime { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con la entidad especificada.
        /// </summary>
        /// <param name="entity">La entidad a actualizar.</param>
        /// <param name="trySetUpdateDatetime">Indica si se debe establecer la fecha de actualización.</param>
        public UpdateEntity_Command (EntityType entity, bool trySetUpdateDatetime = false) {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            TrySetUpdateDatetime = trySetUpdateDatetime;
        }

    }

}