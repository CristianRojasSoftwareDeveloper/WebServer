using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Commands {

    /// <summary>
    /// Representa un comando para eliminar un rol específico por su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.DeleteRoleByID, Enumerations.Permissions.DeleteEntityByID])]
    public class DeleteRoleByID_Command : Operation {

        /// <summary>
        /// Obtiene el ID del rol que se va a eliminar.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el ID del rol especificado.
        /// </summary>
        /// <param name="roleID">El ID del rol que se va a eliminar.</param>
        public DeleteRoleByID_Command (int roleID) {
            RoleID = roleID;
        }

    }

}