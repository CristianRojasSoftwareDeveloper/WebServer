using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener un rol por su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetRoleByID, Enumerations.Permissions.GetEntityByID])]
    public class GetRoleByID_Query : Operation {

        /// <summary>
        /// Obtiene el ID del rol a consultar.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del rol especificado.
        /// </summary>
        /// <param name="roleID">El ID del rol a recuperar.</param>
        public GetRoleByID_Query (int roleID) {
            RoleID = roleID;
        }

    }

}
