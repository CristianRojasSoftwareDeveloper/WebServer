using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users;
using System.Text.RegularExpressions;

namespace Users.Application.Operators.Users.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de actualización de un usuario.
    /// </summary>
    public class UpdateUser_CommandHandler : ISyncOperationHandler<UpdateUser_Command, User>, IAsyncOperationHandler<UpdateUser_Command, User> {

        private IUserRepository _userRepository { get; }
        private IAuthService _authService { get; }

        public UpdateUser_CommandHandler (IUserRepository userRepository, IAuthService authService) {
            _userRepository = userRepository;
            _authService = authService;
        }

        /// <summary>
        /// Maneja el comando de actualización de un usuario de forma síncrona.
        /// </summary>
        /// <param name="command">El comando de actualización de usuario.</param>
        /// <returns>El usuario actualizado.</returns>
        public User Handle (UpdateUser_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el usuario es nulo
            if (command.User == null)
                throw BadRequestError.Create("El usuario por actualizar no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del usuario existe
            if (command.User.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.User.ID), "El identificador del usuario no es válido"));

            // Verificar si el nombre de usuario es vacío (si está presente)
            if (command.User.Username != null)
                if (string.IsNullOrWhiteSpace(command.User.Username))
                    validationErrors.Add(ValidationError.Create(nameof(command.User.Username), "El nombre de usuario no puede ser vacío"));
                else if (_userRepository.GetUserByUsername(command.User.Username) != null)
                    validationErrors.Add(ValidationError.Create(nameof(command.User.Username), $"El nombre de usuario '{command.User.Username}' ya existe"));

            // Verificar si el nombre del usuario es vacío (si está presente)
            if (command.User.Name != null && string.IsNullOrWhiteSpace(command.User.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.User.Name), "El nombre de pila del usuario no puede ser vacío"));

            // Verificar si el correo es nulo o vacío, o si tiene formato válido
            if (command.User.Email != null)
                if (string.IsNullOrWhiteSpace(command.User.Email))
                    validationErrors.Add(ValidationError.Create(nameof(command.User.Email), "El email no puede ser vacío"));
                else if (!Regex.IsMatch(command.User.Email, @"^[a-zA-Z][a-zA-Z0-9_.]*[a-zA-Z]$@[a-zA-Z]+\.[a-zA-Z]{2,}$"))
                    validationErrors.Add(ValidationError.Create(nameof(command.User.Email), "El formato del email no es válido"));

            // Verificar si la contraseña del usuario es nula o vacía (si está presente)
            if (command.User.Password != null && string.IsNullOrWhiteSpace(command.User.Password))
                validationErrors.Add(ValidationError.Create(nameof(command.User.Password), "La contraseña del usuario no puede ser vacía"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Si la contraseña no es nula o vacía, hashearla y asignarla
            command.User.EncryptedPassword = !string.IsNullOrWhiteSpace(command.User.Password) ? _authService.HashPassword(command.User.Password) : null;

            // Ejecutar la actualización del usuario
            return _userRepository.UpdateUser(command.User);

        }

        /// <summary>
        /// Maneja el comando de actualización de un usuario de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de actualización de usuario.</param>
        /// <returns>El usuario actualizado.</returns>
        public async Task<User> HandleAsync (UpdateUser_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el usuario es nulo
            if (command.User == null)
                throw BadRequestError.Create("El usuario por actualizar no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del usuario existe
            if (command.User.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.User.ID), "El identificador del usuario no es válido"));

            // Verificar si el nombre de usuario es vacío (si está presente)
            if (command.User.Username != null)
                if (string.IsNullOrWhiteSpace(command.User.Username))
                    validationErrors.Add(ValidationError.Create(nameof(command.User.Username), "El nombre de usuario no puede ser vacío"));
                else if ((await _userRepository.GetUserByUsernameAsync(command.User.Username)) != null)
                    validationErrors.Add(ValidationError.Create(nameof(command.User.Username), $"El nombre de usuario '{command.User.Username}' ya existe"));

            // Verificar si el nombre del usuario es vacío (si está presente)
            if (command.User.Name != null && string.IsNullOrWhiteSpace(command.User.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.User.Name), "El nombre de pila del usuario no puede ser vacío"));

            // Verificar si el correo es nulo o vacío, o si tiene formato válido
            if (command.User.Email != null)
                if (string.IsNullOrWhiteSpace(command.User.Email))
                    validationErrors.Add(ValidationError.Create(nameof(command.User.Email), "El email no puede ser vacío"));
                else if (!Regex.IsMatch(command.User.Email, @"^[a-zA-Z][a-zA-Z0-9_.]*[a-zA-Z]$@[a-zA-Z]+\.[a-zA-Z]{2,}$"))
                    validationErrors.Add(ValidationError.Create(nameof(command.User.Email), "El formato del email no es válido"));

            // Verificar si la contraseña del usuario es nula o vacía (si está presente)
            if (command.User.Password != null && string.IsNullOrWhiteSpace(command.User.Password))
                validationErrors.Add(ValidationError.Create(nameof(command.User.Password), "La contraseña del usuario no puede ser vacía"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Si la contraseña no es nula o vacía, hashearla y asignarla
            command.User.EncryptedPassword = !string.IsNullOrWhiteSpace(command.User.Password) ? _authService.HashPassword(command.User.Password) : null;

            // Ejecutar la actualización del usuario
            return await _userRepository.UpdateUserAsync(command.User);

        }

    }

}