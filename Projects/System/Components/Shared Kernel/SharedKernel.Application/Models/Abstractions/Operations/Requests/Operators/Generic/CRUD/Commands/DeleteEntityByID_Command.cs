using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands {

    /// <summary>
    /// Comando para eliminar una entidad existente por su ID.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad a eliminar.</typeparam>
    [RequiredPermissions(Enumerations.Permissions.DeleteEntityByID)]
    public class DeleteEntityByID_Command<EntityType> : Operation where EntityType : IGenericEntity {

        /// <summary>
        /// Obtiene el ID de la entidad a eliminar.
        /// </summary>
        public int EntityID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para eliminar una entidad por su ID.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a eliminar.</param>
        public DeleteEntityByID_Command (int entityID) {
            EntityID = entityID;
        }

    }

}