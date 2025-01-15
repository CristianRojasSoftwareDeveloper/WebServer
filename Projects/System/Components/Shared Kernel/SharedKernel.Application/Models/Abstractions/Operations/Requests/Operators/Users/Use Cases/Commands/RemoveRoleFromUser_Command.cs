using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Commands {

    /// <summary>
    /// Comando para eliminar un rol de un usuario.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.RemoveRoleFromUser])]
    public class RemoveRoleFromUser_Command : Operation {

        /// <summary>
        /// Obtiene el ID del usuario del que se eliminará el rol.
        /// </summary>
        public int UserID { get; }

        /// <summary>
        /// Obtiene el ID del rol que se eliminará del usuario.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para eliminar un rol de un usuario.
        /// </summary>
        /// <param name="userID">El ID del usuario del que se eliminará el rol.</param>
        /// <param name="roleID">El ID del rol que se eliminará del usuario.</param>
        public RemoveRoleFromUser_Command (int userID, int roleID) {
            UserID = userID;
            RoleID = roleID;
        }

    }

}