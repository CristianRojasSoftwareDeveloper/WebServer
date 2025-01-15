using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.UseCases.Queries {

    /// <summary>
    /// Consulta para obtener los permisos asociados a un rol mediante su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetPermissionsByRoleID, Enumerations.Permissions.GetEntities])]
    public class GetPermissionsByRoleID_Query : Operation {

        /// <summary>
        /// Obtiene el ID del rol para el cual se solicitan los permisos.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del rol especificado.
        /// </summary>
        /// <param name="roleID">El ID del rol para el cual se solicitan los permisos.</param>
        public GetPermissionsByRoleID_Query (int roleID) => RoleID = roleID;

    }

}