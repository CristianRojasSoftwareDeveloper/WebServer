using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SystemLogs.Application.Operators.SystemLogs.Operations;

namespace SystemLogs.Application.Operators.SystemLogs {

    /// <summary>
    /// Implementación específica de la interfaz <see cref="ISystemLogOperator"/> para operaciones relacionadas con la gestión de los permisos de usuario.
    /// </summary>
    public class SystemLogOperator : GenericOperator<SystemLog>, ISystemLogOperator {

        /// <summary>
        /// Casos de uso específicos para permisos.
        /// </summary>
        private SystemLogs_OperationHandlers _useCases { get; }

        /// <summary>
        /// Constructor que inicializa el operador de logs de sistema.
        /// </summary>
        /// <param name="systemLogRepository">Repositorio de operaciones de logs de sistema utilizado por los casos de uso.</param>
        /// <param name="userRepository">Repositorio de operaciones de usuarios utilizado por los casos de uso.</param>
        /// <param name="detailedLog">Indica si se debe utilizar un registro detallado.</param>
        public SystemLogOperator (ISystemLogRepository systemLogRepository, IUserRepository userRepository, bool detailedLog = false) : base((IGenericRepository<SystemLog>) systemLogRepository, detailedLog)
            => _useCases = new SystemLogs_OperationHandlers(systemLogRepository, userRepository);

        #region Métodos asíncronos

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<SystemLog>> AddSystemLog (IAddSystemLog_Command command)
            => Executor.ExecuteOperation(_useCases.AddSystemLog.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<SystemLog>>> GetSystemLogs (IGetSystemLogs_Query query)
            => GetEntities(new GetEntities_Query(query.EnableTracking));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<SystemLog>> GetSystemLogByID (IGetSystemLogByID_Query query)
            => GetEntityByID(new GetEntityByID_Query(query.ID, query.EnableTracking));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<SystemLog>>> GetSystemLogsByUserID (IGetSystemLogsByUserID_Query query)
            => Executor.ExecuteOperation(_useCases.GetSystemLogsByUserID.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<SystemLog>> UpdateSystemLog (IUpdateSystemLog_Command command)
            => Executor.ExecuteOperation(_useCases.UpdateSystemLog.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> DeleteSystemLogByID (IDeleteSystemLogByID_Command command)
            => DeleteEntityByID(new DeleteEntityByID_Command(command.ID));

        #endregion

    }

}