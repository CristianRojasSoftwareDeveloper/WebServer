using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de actualización de un rol.
    /// </summary>
    public class UpdateRole_CommandHandler : ISyncOperationHandler<UpdateRole_Command, Role>, IAsyncOperationHandler<UpdateRole_Command, Role> {

        private IRoleRepository _roleRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de actualización de roles.
        /// </summary>
        /// <param name="roleRepository">El repositorio de roles.</param>
        public UpdateRole_CommandHandler (IRoleRepository roleRepository) =>
            _roleRepository = roleRepository;

        /// <summary>
        /// Maneja el comando de actualización de un rol de forma síncrona.
        /// </summary>
        /// <param name="command">El comando de actualización de rol.</param>
        /// <returns>El rol actualizado.</returns>
        public Role Handle (UpdateRole_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el rol es nulo
            if (command.Role == null)
                throw BadRequestError.Create("El rol no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del rol existe
            if (command.Role.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.Role.ID), "El identificador del rol de usuario no es válido"));

            // Verificar si el nombre del rol es vacío (si está presente)
            if (command.Role.Name != null)
                if (string.IsNullOrWhiteSpace(command.Role.Name))
                    validationErrors.Add(ValidationError.Create(nameof(command.Role.Name), "El nombre del rol de usuario no puede ser vacío"));
                else if (_roleRepository.FirstOrDefault(role => role.Name!.Equals(command.Role.Name)) != null)
                    validationErrors.Add(ValidationError.Create(nameof(command.Role.Name), $"El nombre del rol de usuario '{command.Role.Name}' ya existe"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecutar la actualización del rol
            return _roleRepository.UpdateRole(command.Role);

        }

        /// <summary>
        /// Maneja el comando de actualización de un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de actualización de rol.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el rol actualizado.</returns>
        public async Task<Role> HandleAsync (UpdateRole_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el rol es nulo
            if (command.Role == null)
                throw BadRequestError.Create("El rol no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del rol existe
            if (command.Role.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.Role.ID), "El identificador del rol de usuario no es válido"));

            // Verificar si el nombre del rol es vacío (si está presente)
            if (command.Role.Name != null)
                if (string.IsNullOrWhiteSpace(command.Role.Name))
                    validationErrors.Add(ValidationError.Create(nameof(command.Role.Name), "El nombre del rol de usuario no puede ser vacío"));
                else if ((await _roleRepository.FirstOrDefaultAsync(role => role.Name!.Equals(command.Role.Name))) != null)
                    validationErrors.Add(ValidationError.Create(nameof(command.Role.Name), $"El nombre del rol de usuario '{command.Role.Name}' ya existe"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecutar la actualización del rol
            return await _roleRepository.UpdateRoleAsync(command.Role);

        }

    }

}