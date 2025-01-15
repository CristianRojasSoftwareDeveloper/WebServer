using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Commands {
    /// <summary>
    /// Comando para actualizar un log del sistema.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.UpdateSystemLog, Enumerations.Permissions.UpdateEntity])]
    public class UpdateSystemLog_Command : Operation {

        /// <summary>
        /// Obtiene el registro de sistema que se va a actualizar.
        /// </summary>
        public SystemLog SystemLog { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el registro de sistema especificado.
        /// </summary>
        /// <param name="systemLog">El registro de sistema a actualizar.</param>
        public UpdateSystemLog_Command (SystemLog systemLog) {
            SystemLog = systemLog;
        }
    }

}