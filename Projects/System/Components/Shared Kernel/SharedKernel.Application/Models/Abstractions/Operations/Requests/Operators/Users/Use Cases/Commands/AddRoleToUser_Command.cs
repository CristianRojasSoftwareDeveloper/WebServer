using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Commands {

    /// <summary>
    /// Comando para agregar un rol a un usuario.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.AddRoleToUser])]
    public class AddRoleToUser_Command : Operation {

        /// <summary>
        /// Obtiene el ID del usuario al que se le agregará el rol.
        /// </summary>
        public int UserID { get; }

        /// <summary>
        /// Obtiene el ID del rol que se agregará al usuario.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para agregar un rol a un usuario.
        /// </summary>
        /// <param name="userID">El ID del usuario al que se le agregará el rol.</param>
        /// <param name="roleID">El ID del rol que se agregará al usuario.</param>
        public AddRoleToUser_Command (int userID, int roleID) {
            UserID = userID;
            RoleID = roleID;
        }

    }

}