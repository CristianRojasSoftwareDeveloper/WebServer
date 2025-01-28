using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.Use_Cases.Queries;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Permissions.Operations.Use_Cases.Queries.GetPermissionsByRoleID {

    /// <summary>
    /// Consulta para obtener los permisos asociados a un rol mediante su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetPermissionsByRoleID, SystemPermissions.GetEntities])]
    [AssociatedOperator(typeof(IPermissionOperator))]
    public class GetPermissionsByRoleID_Query : IGetPermissionsByRoleID_Query {

        /// <summary>
        /// Obtiene el ID del rol para el cual se solicitan los permisos.
        /// </summary>
        public int RoleID { get; }

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del rol especificado.
        /// </summary>
        /// <param name="roleID">El ID del rol para el cual se solicitan los permisos.</param>
        public GetPermissionsByRoleID_Query (int roleID, bool enableTracking = false) {
            RoleID = roleID;
            EnableTracking = enableTracking;
        }

    }

}