using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Commands {

    /// <summary>
    /// Comando para agregar un permiso a un rol.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.AddPermissionToRole])]
    public class AddPermissionToRole_Command : Operation {

        /// <summary>
        /// Obtiene el ID del rol al que se agregará el permiso.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Obtiene el ID del permiso que se agregará al rol.
        /// </summary>
        public int PermissionID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para agregar un permiso a un rol.
        /// </summary>
        /// <param name="roleID">El ID del rol al que se agregará el permiso.</param>
        /// <param name="permissionID">El ID del permiso que se agregará al rol.</param>
        public AddPermissionToRole_Command (int roleID, int permissionID) {
            RoleID = roleID;
            PermissionID = permissionID;
        }

    }

}