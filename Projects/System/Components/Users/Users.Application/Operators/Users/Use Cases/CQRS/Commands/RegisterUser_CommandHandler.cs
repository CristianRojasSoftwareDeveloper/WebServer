using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users;
using System.Net;
using System.Text.RegularExpressions;

namespace Users.Application.Operators.Users.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de registro de usuario.
    /// </summary>
    public class RegisterUser_CommandHandler : ISyncOperationHandler<RegisterUser_Command, User>, IAsyncOperationHandler<RegisterUser_Command, User> {

        private IUserRepository _userRepository { get; }

        private IAuthService _authService { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comando de registro de usuario.
        /// </summary>
        /// <param name="userRepository">Repositorio de usuarios para acceder a la capa de persistencia.</param>
        /// <param name="authService">Servicio de autenticación para el manejo de contraseñas.</param>
        public RegisterUser_CommandHandler (IUserRepository userRepository, IAuthService authService) {
            _userRepository = userRepository;
            _authService = authService;
        }

        /// <summary>
        /// Maneja la operación sincrónica de registro de usuario.
        /// </summary>
        /// <param name="command">El comando de registro de usuario.</param>
        /// <returns>El usuario registrado.</returns>
        public User Handle (RegisterUser_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                // Lanzar un Error de aplicación si el comando es nulo
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el usuario en el comando es nulo
            if (command.User == null)
                // Lanzar un Error de aplicación si el usuario es nulo
                throw BadRequestError.Create("El usuario no puede ser nulo");

            // Crear una lista para almacenar posibles errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el nombre de usuario es nulo o está vacío
            if (string.IsNullOrWhiteSpace(command.User.Username))
                // Agregar un Error de validación si el nombre de usuario es nulo o vacío
                validationErrors.Add(ValidationError.Create(nameof(command.User.Username), "El nombre de usuario no puede ser nulo o vacío"));
            else if (_userRepository.GetUserByUsername(command.User.Username) != null)
                // Verificar si el nombre de usuario ya existe en el repositorio y agregar un Error de validación si es así
                validationErrors.Add(ValidationError.Create(nameof(command.User.Username), $"El nombre de usuario '{command.User.Username}' ya existe"));

            // Verificar si el nombre del usuario es nulo o está vacío
            if (string.IsNullOrWhiteSpace(command.User.Name))
                // Agregar un Error de validación si el nombre del usuario es nulo o vacío
                validationErrors.Add(ValidationError.Create(nameof(command.User.Name), "El nombre de pila del usuario no puede ser nulo o vacío"));

            // Verificar si el correo electrónico es nulo, vacío o tiene un formato no válido
            if (string.IsNullOrWhiteSpace(command.User.Email))
                // Agregar un Error de validación si el correo es nulo o vacío
                validationErrors.Add(ValidationError.Create(nameof(command.User.Email), "El email no puede ser nulo o vacío"));
            else if (!Regex.IsMatch(command.User.Email, @"^[a-zA-Z][a-zA-Z0-9_.]*[a-zA-Z]@[a-zA-Z]+\.[a-zA-Z]{2,}$"))
                // Agregar un Error de validación si el formato del correo es inválido
                validationErrors.Add(ValidationError.Create(nameof(command.User.Email), "El formato del email no es válido"));

            // Verificar si la contraseña del usuario es nula o está vacía
            if (string.IsNullOrWhiteSpace(command.User.Password))
                // Agregar un Error de validación si la contraseña es nula o vacía
                validationErrors.Add(ValidationError.Create(nameof(command.User.Password), "La contraseña del usuario no puede ser nula o vacía"));

            // Si hay errores de validación, lanzar un Error agregado que contiene todos los errores
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Hashear la contraseña del usuario y asignarla al campo EncryptedPassword del usuario
            command.User.EncryptedPassword = _authService.HashPassword(command.User.Password!);

            // Agregar el usuario al repositorio y obtener el usuario registrado
            var registeredUser = _userRepository.AddUser(command.User);
            // Verificar si el usuario registrado es nulo
            if (registeredUser == null)
                // Lanzar un Error de aplicación si ocurrió un Error durante el registro del usuario
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, $"Ha ocurrido un error mientras se registraba al usuario «{command.User.Username}»");

            // Devolver el usuario registrado
            return registeredUser;
        }

        /// <summary>
        /// Maneja la operación asincrónica de registro de usuario.
        /// </summary>
        /// <param name="command">El comando de registro de usuario.</param>
        /// <returns>El usuario registrado.</returns>
        public async Task<User> HandleAsync (RegisterUser_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el usuario es nulo
            if (command.User == null)
                throw BadRequestError.Create("El usuario no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el nombre de usuario es nulo o vacío
            if (string.IsNullOrWhiteSpace(command.User.Username))
                validationErrors.Add(ValidationError.Create(nameof(command.User.Username), "El nombre de usuario no puede ser nulo o vacío"));
            else if ((await _userRepository.GetUserByUsernameAsync(command.User.Username)) != null)
                validationErrors.Add(ValidationError.Create(nameof(command.User.Username), $"El nombre de usuario '{command.User.Username}' ya existe"));

            // Verificar si el nombre es nulo o vacío
            if (string.IsNullOrWhiteSpace(command.User.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.User.Name), "El nombre de pila del usuario no puede ser nulo o vacío"));

            // Verificar si el correo es nulo o vacío, o si tiene formato válido
            if (string.IsNullOrWhiteSpace(command.User.Email))
                validationErrors.Add(ValidationError.Create(nameof(command.User.Email), "El email no puede ser nulo o vacío"));
            else if (!Regex.IsMatch(command.User.Email, @"^[a-zA-Z][a-zA-Z0-9_.]*[a-zA-Z]$@[a-zA-Z]+\.[a-zA-Z]{2,}$"))
                validationErrors.Add(ValidationError.Create(nameof(command.User.Email), "El formato del email no es válido"));

            // Verificar si la contraseña es nula o vacía
            if (string.IsNullOrWhiteSpace(command.User.Password))
                validationErrors.Add(ValidationError.Create(nameof(command.User.Password), "La contraseña del usuario no puede ser nula o vacía"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Hashear la contraseña y asignarla al campo EncryptedPassword del usuario
            command.User.EncryptedPassword = _authService.HashPassword(command.User.Password!);

            // Agregar el usuario al repositorio y obtener el usuario registrado
            var registeredUser = await _userRepository.AddUserAsync(command.User);
            // Verificar si el usuario registrado es nulo
            if (registeredUser == null)
                // Lanzar un Error de aplicación si ocurrió un Error durante el registro del usuario
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, $"Ha ocurrido un Error mientras se registraba al usuario '{command.User.Username}'");

            // Devolver el usuario registrado
            return registeredUser;
        }

    }

}