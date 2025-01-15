using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Commands {

    /// <summary>
    /// Comando para eliminar un permiso de un rol.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.RemovePermissionFromRole])]
    public class RemovePermissionFromRole_Command : Operation {

        /// <summary>
        /// Obtiene el ID del rol del que se eliminará el permiso.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Obtiene el ID del permiso que se eliminará del rol.
        /// </summary>
        public int PermissionID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para eliminar un permiso de un rol.
        /// </summary>
        /// <param name="roleID">El ID del rol del que se eliminará el permiso.</param>
        /// <param name="permissionID">El ID del permiso que se eliminará del rol.</param>
        public RemovePermissionFromRole_Command (int roleID, int permissionID) {
            RoleID = roleID;
            PermissionID = permissionID;
        }

    }

}