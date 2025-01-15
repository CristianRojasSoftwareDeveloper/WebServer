using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Commands {

    /// <summary>
    /// Comando para eliminar un permiso por su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.DeletePermissionByID, Enumerations.Permissions.DeleteEntityByID])]
    public class DeletePermissionByID_Command : Operation {

        /// <summary>
        /// Obtiene el ID del permiso que se va a eliminar.
        /// </summary>
        public int PermissionID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el ID del permiso especificado.
        /// </summary>
        /// <param name="permissionID">El ID del permiso que se va a eliminar.</param>
        public DeletePermissionByID_Command (int permissionID) {
            PermissionID = permissionID;
        }

    }

}