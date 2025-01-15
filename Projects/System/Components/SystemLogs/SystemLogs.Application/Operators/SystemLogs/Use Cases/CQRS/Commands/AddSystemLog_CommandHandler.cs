using SharedKernel.Application.Models.Abstractions;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Commands;

namespace SystemLogs.Application.Operators.SystemLogs.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de agregar un nuevo log de sistema.
    /// </summary>
    public class AddSystemLog_CommandHandler : ISyncOperationHandler<AddSystemLog_Command, SystemLog>, IAsyncOperationHandler<AddSystemLog_Command, SystemLog> {

        private ISystemLogRepository _systemLogRepository { get; }
        private IUserRepository _userRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de agregar un nuevo log de sistema.
        /// </summary>
        /// <param name="systemLogRepository">Repositorio de logs de sistema.</param>
        public AddSystemLog_CommandHandler (ISystemLogRepository systemLogRepository, IUserRepository userRepository) {
            _systemLogRepository = systemLogRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Maneja de forma síncrona el comando para agregar un nuevo log de sistema.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo log de sistema.</param>
        /// <returns>El log de sistema agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el log de sistema son nulos.</exception>
        /// <exception cref="AggregateError">Se lanza si hay errores de validación.</exception>
        public SystemLog Handle (AddSystemLog_Command command) {
            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el log de sistema es nulo
            if (command.SystemLog == null)
                throw BadRequestError.Create("El log de sistema no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si las propiedades de SystemLog contienen valores no vacíos y válidos.
            if (command.SystemLog.LogLevel == null)
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.LogLevel), "El nivel de severidad del registro no puede ser nulo"));
            if (string.IsNullOrWhiteSpace(command.SystemLog.Source))
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Source), "El origen del registro no puede ser nulo o vacío"));
            if (string.IsNullOrWhiteSpace(command.SystemLog.Message))
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Message), "El mensaje del registro no puede ser nulo o vacío"));
            if (command.SystemLog.UserID != null && _userRepository.GetUserByID((int) command.SystemLog.UserID) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Message), $"No se ha encontrado el usuario con el ID {command.SystemLog.UserID}. No se puede asociar este log del sistema con un usuario inexistente."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Agregar el log de sistema y devolverlo
            return _systemLogRepository.AddSystemLog(command.SystemLog);
        }

        /// <summary>
        /// Maneja de forma asíncrona el comando para agregar un nuevo log de sistema.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo log de sistema.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el log de sistema agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el log de sistema son nulos.</exception>
        /// <exception cref="AggregateError">Se lanza si hay errores de validación.</exception>
        public async Task<SystemLog> HandleAsync (AddSystemLog_Command command) {
            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el log de sistema es nulo
            if (command.SystemLog == null)
                throw BadRequestError.Create("El log de sistema no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si las propiedades de SystemLog contienen valores no vacíos y válidos.
            if (command.SystemLog.LogLevel == null)
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.LogLevel), "El nivel de severidad del registro no puede ser nulo"));
            if (string.IsNullOrWhiteSpace(command.SystemLog.Source))
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Source), "El origen del registro no puede ser nulo o vacío"));
            if (string.IsNullOrWhiteSpace(command.SystemLog.Message))
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Message), "El mensaje del registro no puede ser nulo o vacío"));
            if (command.SystemLog.UserID != null && (await _userRepository.GetUserByIDAsync((int) command.SystemLog.UserID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.SystemLog.Message), $"No se ha encontrado el usuario con el ID {command.SystemLog.UserID}. No se puede asociar este log del sistema con un usuario inexistente."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Agregar el log de sistema de forma asíncrona y devolverlo
            return await _systemLogRepository.AddSystemLogAsync(command.SystemLog);
        }

    }

}