using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener todos los roles.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetRoles, Enumerations.Permissions.GetEntities])]
    public class GetRoles_Query : Operation {

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todos los roles.
        /// </summary>
        public GetRoles_Query () { }

    }

}