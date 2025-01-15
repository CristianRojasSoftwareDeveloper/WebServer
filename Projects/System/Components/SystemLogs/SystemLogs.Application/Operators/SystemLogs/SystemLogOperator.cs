using SharedKernel.Application.Models.Abstractions;
using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Generic.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.UseCases.Queries;
using SharedKernel.Application.Operators.Generic;
using SystemLogs.Application.Operators.SystemLogs.UseCases;

namespace SystemLogs.Application.Operators.SystemLogs {

    /// <summary>
    /// Implementación específica de la interfaz <see cref="ISystemLogOperator"/> para operaciones relacionadas con la gestión de los permisos de usuario.
    /// </summary>
    public class SystemLogOperator : GenericOperator<SystemLog>, ISystemLogOperator {

        /// <summary>
        /// Casos de uso específicos para permisos.
        /// </summary>
        private SystemLogs_UseCases _useCases { get; }

        /// <summary>
        /// Constructor que inicializa el operador de logs de sistema.
        /// </summary>
        /// <param name="systemLogRepository">Repositorio de operaciones de logs de sistema utilizado por los casos de uso.</param>
        /// <param name="userRepository">Repositorio de operaciones de usuarios utilizado por los casos de uso.</param>
        /// <param name="detailedLog">Indica si se debe utilizar un registro detallado.</param>
        public SystemLogOperator (ISystemLogRepository systemLogRepository, IUserRepository userRepository, bool detailedLog = false) : base((IGenericRepository<SystemLog>) systemLogRepository, detailedLog)
            => _useCases = new SystemLogs_UseCases(systemLogRepository, userRepository);

        #region Métodos síncronos

        /// <inheritdoc />
        [OperationHandler]
        public Response<SystemLog> AddSystemLog (AddSystemLog_Command command)
            => Executor.ExecuteSynchronousOperation(_useCases.AddSystemLog.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<List<SystemLog>> GetSystemLogs (GetSystemLogs_Query query)
            => GetEntities(new GetEntities_Query<SystemLog>());

        /// <inheritdoc />
        [OperationHandler]
        public Response<SystemLog> GetSystemLogByID (GetSystemLogByID_Query query)
            => GetEntityByID(new GetEntityByID_Query<SystemLog>(query.SystemLogID));

        /// <inheritdoc />
        [OperationHandler]
        public Response<List<SystemLog>> GetSystemLogsByUserID (GetSystemLogsByUserID_Query query)
            => Executor.ExecuteSynchronousOperation(_useCases.GetSystemLogsByUserID.Handle, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<SystemLog> UpdateSystemLog (UpdateSystemLog_Command command)
            => Executor.ExecuteSynchronousOperation(_useCases.UpdateSystemLog.Handle, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Response<bool> DeleteSystemLogByID (DeleteSystemLogByID_Command command)
            => DeleteEntityByID(new DeleteEntityByID_Command<SystemLog>(command.SystemLogID));

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<SystemLog>> AddSystemLogAsync (AddSystemLog_Command command)
            => Executor.ExecuteAsynchronousOperation(_useCases.AddSystemLog.HandleAsync, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<SystemLog>>> GetSystemLogsAsync (GetSystemLogs_Query query)
            => GetEntitiesAsync(new GetEntities_Query<SystemLog>());

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<SystemLog>> GetSystemLogByIDAsync (GetSystemLogByID_Query query)
            => GetEntityByIDAsync(new GetEntityByID_Query<SystemLog>(query.SystemLogID));

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<List<SystemLog>>> GetSystemLogsByUserIDAsync (GetSystemLogsByUserID_Query query)
            => Executor.ExecuteAsynchronousOperation(_useCases.GetSystemLogsByUserID.HandleAsync, query, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<SystemLog>> UpdateSystemLogAsync (UpdateSystemLog_Command command)
            => Executor.ExecuteAsynchronousOperation(_useCases.UpdateSystemLog.HandleAsync, command, _detailedLog);

        /// <inheritdoc />
        [OperationHandler]
        public Task<Response<bool>> DeleteSystemLogByIDAsync (DeleteSystemLogByID_Command command)
            => DeleteEntityByIDAsync(new DeleteEntityByID_Command<SystemLog>(command.SystemLogID));

        #endregion

    }

}