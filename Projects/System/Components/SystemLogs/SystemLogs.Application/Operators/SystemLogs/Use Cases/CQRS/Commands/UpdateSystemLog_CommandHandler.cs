using SharedKernel.Application.Models.Abstractions;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Commands;

namespace SystemLogs.Application.Operators.SystemLogs.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de actualización de log de sistema.
    /// </summary>
    public class UpdateSystemLog_CommandHandler : ISyncOperationHandler<UpdateSystemLog_Command, SystemLog>, IAsyncOperationHandler<UpdateSystemLog_Command, SystemLog> {

        private ISystemLogRepository _systemLogRepository { get; }
        private IUserRepository _userRepository { get; }

        public UpdateSystemLog_CommandHandler (ISystemLogRepository systemLogRepository, IUserRepository userRepository) {
            _systemLogRepository = systemLogRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Maneja el comando de actualización de un log de sistema de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el log de sistema a actualizar.</param>
        /// <returns>El log de sistema actualizado.</returns>
        public SystemLog Handle (UpdateSystemLog_Command command) {
            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el log de sistema es nulo
            if (command.SystemLog == null)
                throw BadRequestError.Create("El log de sistema no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del log de sistema es válido
            if (command.SystemLog.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.ID), "El identificador del log de sistema de usuario no es válido"));

            // Verificar si las propiedades de SystemLog contienen valores no vacíos y válidos.
            if (command.SystemLog.Source != null && string.IsNullOrWhiteSpace(command.SystemLog.Source))
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Source), "El origen del registro no puede ser vacío"));
            if (command.SystemLog.Message != null && string.IsNullOrWhiteSpace(command.SystemLog.Message))
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Message), "El mensaje del registro no puede ser vacío"));
            if (command.SystemLog.UserID != null && _userRepository.GetUserByID((int) command.SystemLog.UserID) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Message), $"No se ha encontrado el usuario con el ID {command.SystemLog.UserID}. No se puede asociar este log del sistema con un usuario inexistente."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecutar la actualización del log de sistema
            return _systemLogRepository.UpdateSystemLog(command.SystemLog);

        }

        /// <summary>
        /// Maneja el comando de actualización de un log de sistema de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el log de sistema a actualizar.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene el log de sistema actualizado.</returns>
        public async Task<SystemLog> HandleAsync (UpdateSystemLog_Command command) {
            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el log de sistema es nulo
            if (command.SystemLog == null)
                throw BadRequestError.Create("El log de sistema no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del log de sistema es válido
            if (command.SystemLog.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.ID), "El identificador del log de sistema de usuario no es válido"));

            // Verificar si el nombre del log de sistema no está vacío
            if (command.SystemLog.Source != null && string.IsNullOrWhiteSpace(command.SystemLog.Source))
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Source), "El origen del registro no puede ser vacío"));
            if (command.SystemLog.Message != null && string.IsNullOrWhiteSpace(command.SystemLog.Message))
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Message), "El mensaje del registro no puede ser vacío"));
            if (command.SystemLog.UserID != null && (await _userRepository.GetUserByIDAsync((int) command.SystemLog.UserID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Message), $"No se ha encontrado el usuario con el ID {command.SystemLog.UserID}. No se puede asociar este log del sistema con un usuario inexistente."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecutar la actualización del log de sistema de forma asíncrona
            return await _systemLogRepository.UpdateSystemLogAsync(command.SystemLog);

        }

    }

}