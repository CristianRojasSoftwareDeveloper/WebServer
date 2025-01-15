using SharedKernel.Application.Models.Abstractions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.UseCases.Queries;

namespace SystemLogs.Application.Operators.SystemLogs.UseCases.CQRS.Queries {

    /// <summary>
    /// Manejador para la consulta de obtención de logs del sistema asociados a un usuario por su ID.
    /// </summary>
    public class GetSystemLogsByUserID_QueryHandler : ISyncOperationHandler<GetSystemLogsByUserID_Query, List<SystemLog>>, IAsyncOperationHandler<GetSystemLogsByUserID_Query, List<SystemLog>> {

        private ISystemLogRepository _systemLogRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consulta.
        /// </summary>
        /// <param name="systemLogRepository">El repositorio de logs del sistema.</param>
        public GetSystemLogsByUserID_QueryHandler (ISystemLogRepository systemLogRepository) =>
            _systemLogRepository = systemLogRepository;

        /// <summary>
        /// Maneja la consulta de obtención de logs del sistema asociados a un usuario por su ID de manera sincrónica.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del usuario.</param>
        /// <returns>Una lista de logs del sistema asociados al usuario especificado.</returns>
        public List<SystemLog> Handle (GetSystemLogsByUserID_Query query) =>
            _systemLogRepository.GetSystemLogsByUserID(query.UserID);

        /// <summary>
        /// Maneja la consulta de obtención de logs del sistema asociados a un usuario por su ID de manera asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene una lista de logs del sistema asociados al usuario especificado.</returns>
        public Task<List<SystemLog>> HandleAsync (GetSystemLogsByUserID_Query query) =>
            _systemLogRepository.GetSystemLogsByUserIDAsync(query.UserID);

    }

}