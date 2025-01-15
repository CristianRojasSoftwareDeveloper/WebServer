using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener un log del sistema por su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetSystemLogByID, Enumerations.Permissions.GetEntityByID])]
    public class GetSystemLogByID_Query : Operation {

        /// <summary>
        /// ID del log del sistema que se intenta obtener.
        /// </summary>
        public int SystemLogID { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del log del sistema especificado.
        /// </summary>
        /// <param name="systemLogID">El ID del log del sistema que se desea recuperar.</param>
        public GetSystemLogByID_Query (int systemLogID) {
            SystemLogID = systemLogID;
        }

    }

}