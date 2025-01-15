using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Commands {

    /// <summary>
    /// Comando para eliminar un log del sistema por su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.DeleteSystemLogByID, Enumerations.Permissions.DeleteEntityByID])]
    public class DeleteSystemLogByID_Command : Operation {

        /// <summary>
        /// Obtiene el ID del registro de sistema que se va a eliminar.
        /// </summary>
        public int SystemLogID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el ID del registro de sistema especificado.
        /// </summary>
        /// <param name="systemLogID">El ID del registro de sistema que se va a eliminar.</param>
        public DeleteSystemLogByID_Command (int systemLogID) {
            SystemLogID = systemLogID;
        }

    }

}