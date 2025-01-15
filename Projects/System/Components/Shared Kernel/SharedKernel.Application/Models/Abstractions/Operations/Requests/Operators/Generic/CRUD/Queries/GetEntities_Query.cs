using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener todos los registros de una determinada entidad.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad a consultar.</typeparam>
    [RequiredPermissions(Enumerations.Permissions.GetEntities)]
    public class GetEntities_Query<EntityType> : Operation where EntityType : IGenericEntity {

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todas las entidades.
        /// </summary>
        public GetEntities_Query () { }

    }

}