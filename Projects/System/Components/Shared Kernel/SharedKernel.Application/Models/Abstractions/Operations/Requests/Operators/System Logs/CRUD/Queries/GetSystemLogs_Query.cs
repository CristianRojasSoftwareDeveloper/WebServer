using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener todos los logs del sistema.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetSystemLogs, Enumerations.Permissions.GetEntities])]
    public class GetSystemLogs_Query : Operation {

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todos los registros de sistema.
        /// </summary>
        public GetSystemLogs_Query () { }
    }

}