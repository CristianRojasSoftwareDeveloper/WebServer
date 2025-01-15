using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener una entidad por su ID.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad a consultar.</typeparam>
    [RequiredPermissions(Enumerations.Permissions.GetEntityByID)]
    public class GetEntityByID_Query<EntityType> : Operation where EntityType : IGenericEntity {

        /// <summary>
        /// Obtiene el ID de la entidad a recuperar.
        /// </summary>
        public int EntityID { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener una entidad por su ID.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a consultar.</param>
        public GetEntityByID_Query (int entityID) {
            EntityID = entityID;
        }

    }

}