using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Commands {

    /// <summary>
    /// Comando para eliminar un usuario por su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.DeleteUserByID, Enumerations.Permissions.DeleteEntityByID])]
    public class DeleteUserByID_Command : Operation {

        /// <summary>
        /// Obtiene el ID del usuario a eliminar.
        /// </summary>
        public int UserID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para eliminar un usuario por su ID.
        /// </summary>
        /// <param name="userID">El ID del usuario a eliminar.</param>
        public DeleteUserByID_Command (int userID) => UserID = userID;

    }

}