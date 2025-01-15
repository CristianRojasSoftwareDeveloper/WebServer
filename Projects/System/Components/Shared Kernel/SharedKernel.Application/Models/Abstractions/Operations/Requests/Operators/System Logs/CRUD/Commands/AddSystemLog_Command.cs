using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Commands {

    /// <summary>
    /// Comando para agregar un nuevo log del sistema.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.AddSystemLog, Enumerations.Permissions.AddEntity])]
    public class AddSystemLog_Command : Operation {

        /// <summary>
        /// Obtiene el registro de sistema que se va a agregar.
        /// </summary>
        public SystemLog SystemLog { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el registro de sistema especificado.
        /// </summary>
        /// <param name="systemLog">El registro de sistema a agregar.</param>
        public AddSystemLog_Command (SystemLog systemLog) {
            SystemLog = systemLog;
        }

    }

}