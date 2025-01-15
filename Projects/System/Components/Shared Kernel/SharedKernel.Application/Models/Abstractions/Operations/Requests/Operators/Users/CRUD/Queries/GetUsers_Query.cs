using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener todos los usuarios.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetUsers, Enumerations.Permissions.GetEntities])]
    public class GetUsers_Query : Operation {

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todos los usuarios.
        /// </summary>
        public GetUsers_Query () { }

    }

}