using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener un permiso por su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetPermissionByID, Enumerations.Permissions.GetEntityByID])]
    public class GetPermissionByID_Query : Operation {

        /// <summary>
        /// Obtiene el ID del permiso a recuperar.
        /// </summary>
        public int PermissionID { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del permiso especificado.
        /// </summary>
        /// <param name="permissionID">El ID del permiso a recuperar.</param>
        public GetPermissionByID_Query (int permissionID) {
            PermissionID = permissionID;
        }

    }

}