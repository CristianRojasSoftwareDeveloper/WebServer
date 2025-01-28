using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.Use_Cases.Queries.GetSystemLogsByUserID {

    /// <summary>
    /// Manejador para la consulta de obtención de logs del sistema asociados a un usuario por su ID.
    /// </summary>
    public class GetSystemLogsByUserID_QueryHandler : IOperationHandler<IGetSystemLogsByUserID_Query, List<SystemLog>> {

        private ISystemLogRepository _systemLogRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consulta.
        /// </summary>
        /// <param name="systemLogRepository">El repositorio de logs del sistema.</param>
        public GetSystemLogsByUserID_QueryHandler (ISystemLogRepository systemLogRepository) =>
            _systemLogRepository = systemLogRepository;

        /// <summary>
        /// Maneja la consulta de obtención de logs del sistema asociados a un usuario por su ID de manera asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene una lista de logs del sistema asociados al usuario especificado.</returns>
        public Task<List<SystemLog>> Handle (IGetSystemLogsByUserID_Query query) =>
            _systemLogRepository.GetSystemLogsByUserID(query.UserID, query.EnableTracking);

    }

}