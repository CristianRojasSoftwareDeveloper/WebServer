using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador del comando <see cref="AddRole_Command"/> que agrega un nuevo rol.
    /// </summary>
    public class AddRole_CommandHandler : ISyncOperationHandler<AddRole_Command, Role>, IAsyncOperationHandler<AddRole_Command, Role> {

        /// <summary>
        /// Repositorio de roles para la persistencia.
        /// </summary>
        private IRoleRepository _roleRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador con el repositorio de roles especificado.
        /// </summary>
        /// <param name="roleRepository">El repositorio de roles.</param>
        public AddRole_CommandHandler (IRoleRepository roleRepository) =>
            _roleRepository = roleRepository;

        /// <summary>
        /// Maneja el comando de manera síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene la información del rol a agregar.</param>
        /// <returns>El rol agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el rol son nulos, o si hay errores de validación.</exception>
        public Role Handle (AddRole_Command command) {
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            if (command.Role == null)
                throw BadRequestError.Create("El rol no puede ser nulo");

            var validationErrors = new List<ApplicationError>();

            if (string.IsNullOrWhiteSpace(command.Role.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.Role.Name), "El nombre del rol no puede ser nulo o vacío"));
            else if (_roleRepository.FirstOrDefault(role => role.Name!.Equals(command.Role.Name)) != null)
                validationErrors.Add(ValidationError.Create(nameof(command.Role.Name), $"El nombre del rol '{command.Role.Name}' ya existe"));

            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            return _roleRepository.AddRole(command.Role);
        }

        /// <summary>
        /// Maneja el comando de manera asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene la información del rol a agregar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el rol agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el rol son nulos, o si hay errores de validación.</exception>
        public async Task<Role> HandleAsync (AddRole_Command command) {
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            if (command.Role == null)
                throw BadRequestError.Create("El rol no puede ser nulo");

            var validationErrors = new List<ApplicationError>();

            if (string.IsNullOrWhiteSpace(command.Role.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.Role.Name), "El nombre del rol no puede ser nulo o vacío"));
            else if ((await _roleRepository.FirstOrDefaultAsync(role => role.Name!.Equals(command.Role.Name))) != null)
                validationErrors.Add(ValidationError.Create(nameof(command.Role.Name), $"El nombre del rol '{command.Role.Name}' ya existe"));

            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            return await _roleRepository.AddRoleAsync(command.Role);
        }

    }

}