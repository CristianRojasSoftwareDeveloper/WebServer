using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog;
using SystemLogs.Application.Operators.SystemLogs.Operations.Use_Cases.Queries.GetSystemLogsByUserID;

namespace SystemLogs.Application.Operators.SystemLogs.Operations {

    /// <summary>
    /// Clase que centraliza el acceso a los casos de uso relacionados con los logs del sistema.
    /// Implementa inicialización lazy para optimizar el rendimiento.
    /// </summary>
    public class SystemLogs_OperationHandlers {

        #region Queries (Consultas)

        /// <summary>
        /// Caso de uso para obtener los logs del sistema asociados a un usuario por su ID.
        /// </summary>
        private Lazy<GetSystemLogsByUserID_QueryHandler> _getSystemLogsByUserID { get; }
        public GetSystemLogsByUserID_QueryHandler GetSystemLogsByUserID => _getSystemLogsByUserID.Value;

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Caso de uso para agregar un nuevo log del sistema.
        /// </summary>
        private Lazy<AddSystemLog_CommandHandler> _addSystemLog { get; }
        public AddSystemLog_CommandHandler AddSystemLog => _addSystemLog.Value;

        /// <summary>
        /// Caso de uso para actualizar un log del sistema existente.
        /// </summary>
        private Lazy<UpdateSystemLog_CommandHandler> _updateSystemLog { get; }
        public UpdateSystemLog_CommandHandler UpdateSystemLog => _updateSystemLog.Value;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que inicializa los inicializadores lazy para los casos de uso relacionados con logs del sistema.
        /// </summary>
        /// <param name="systemLogRepository">Repositorio para operaciones de logs del sistema.</param>
        /// <param name="userRepository">Repositorio para operaciones de usuarios.</param>
        public SystemLogs_OperationHandlers (ISystemLogRepository systemLogRepository, IUserRepository userRepository) {
            _getSystemLogsByUserID = new Lazy<GetSystemLogsByUserID_QueryHandler>(() => new GetSystemLogsByUserID_QueryHandler(systemLogRepository));
            _addSystemLog = new Lazy<AddSystemLog_CommandHandler>(() => new AddSystemLog_CommandHandler(systemLogRepository, userRepository));
            _updateSystemLog = new Lazy<UpdateSystemLog_CommandHandler>(() => new UpdateSystemLog_CommandHandler(systemLogRepository, userRepository));
        }

        #endregion

    }

}