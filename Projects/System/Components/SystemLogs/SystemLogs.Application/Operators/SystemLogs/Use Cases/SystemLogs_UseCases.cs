using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SystemLogs.Application.Operators.SystemLogs.UseCases.CQRS.Commands;
using SystemLogs.Application.Operators.SystemLogs.UseCases.CQRS.Queries;

namespace SystemLogs.Application.Operators.SystemLogs.UseCases {

    /// <summary>
    /// Clase que proporciona acceso a todos los casos de uso relacionados con los permisos de usuario.
    /// </summary>
    public class SystemLogs_UseCases {

        #region Queries
        public GetSystemLogsByUserID_QueryHandler GetSystemLogsByUserID { get; }
        #endregion

        #region Commands
        public AddSystemLog_CommandHandler AddSystemLog { get; }
        public UpdateSystemLog_CommandHandler UpdateSystemLog { get; }
        #endregion

        /// <summary>
        /// Constructor que inicializa todos los casos de uso con el repositorio de permisos de usuario proporcionado.
        /// </summary>
        /// <param name="systemLogRepository">Repositorio de operaciones de logs de sistema utilizado por los casos de uso.</param>
        /// <param name="userRepository">Repositorio de operaciones de usuarios utilizado por los casos de uso.</param>
        public SystemLogs_UseCases (ISystemLogRepository systemLogRepository, IUserRepository userRepository) {
            GetSystemLogsByUserID = new GetSystemLogsByUserID_QueryHandler(systemLogRepository);
            AddSystemLog = new AddSystemLog_CommandHandler(systemLogRepository, userRepository);
            UpdateSystemLog = new UpdateSystemLog_CommandHandler(systemLogRepository, userRepository);
        }

    }

}