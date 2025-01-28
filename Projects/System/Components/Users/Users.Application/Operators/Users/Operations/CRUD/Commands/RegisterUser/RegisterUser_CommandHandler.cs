using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Application.Utils.Validators;
using SharedKernel.Domain.Models.Entities.Users;
using System.Net;

namespace Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser {

    /// <summary>
    /// Manejador para el comando de registro de usuario.
    /// </summary>
    public class RegisterUser_CommandHandler : IOperationHandler<IRegisterUser_Command, User> {

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
        /// Maneja la operación asincrónica de registro de usuario.
        /// </summary>
        /// <param name="command">El comando de registro de usuario.</param>
        /// <returns>El usuario registrado.</returns>
        public async Task<User> Handle (IRegisterUser_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el usuario es nulo
            if (command.Entity == null)
                throw BadRequestError.Create("El usuario no puede ser nulo");

            var user = command.Entity;

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el nombre de usuario es nulo o vacío
            if (string.IsNullOrWhiteSpace(user.Username))
                validationErrors.Add(ValidationError.Create(nameof(user.Username), "El nombre de usuario no puede ser nulo o vacío"));
            else if (await _userRepository.GetUserByUsername(user.Username) != null)
                validationErrors.Add(ValidationError.Create(nameof(user.Username), $"El nombre de usuario '{user.Username}' ya existe"));

            // Verificar si el nombre es nulo o vacío
            if (string.IsNullOrWhiteSpace(user.Name))
                validationErrors.Add(ValidationError.Create(nameof(user.Name), "El nombre de pila del usuario no puede ser nulo o vacío"));

            // Verificar si el correo es nulo o vacío, o si tiene formato válido
            if (string.IsNullOrWhiteSpace(user.Email))
                validationErrors.Add(ValidationError.Create(nameof(user.Email), "El email no puede ser nulo o vacío"));
            else if (!EmailValidator.IsValidEmail(user.Email))
                validationErrors.Add(ValidationError.Create(nameof(user.Email), "El formato del email no es válido"));

            // Valida y encripta la contraseña del usuario.
            if (string.IsNullOrWhiteSpace(user.Password))
                validationErrors.Add(ValidationError.Create(nameof(user.Password), "La contraseña del usuario no puede estar vacía."));
            else // Si la contraseña no es nula o vacía se encripta y reasigna.
                user.Password = _authService.HashPassword(user.Password);

            // Si hay errores de validación lanza un «AggregateError».
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Agregar el usuario al repositorio y obtener el usuario registrado
            var registeredUser = await _userRepository.AddUser(user) ??
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, $"Ha ocurrido unerror mientras se registraba al usuario «{user.Username}».");

            // Devolver el usuario registrado
            return registeredUser;
        }

    }

}