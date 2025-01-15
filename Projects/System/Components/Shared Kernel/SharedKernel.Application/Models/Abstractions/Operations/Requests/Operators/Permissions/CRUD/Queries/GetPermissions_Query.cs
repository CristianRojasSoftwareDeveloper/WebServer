using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener todos los permisos.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetPermissions, Enumerations.Permissions.GetEntities])]
    public class GetPermissions_Query : Operation {

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todos los permisos.
        /// </summary>
        public GetPermissions_Query () { }

    }

}