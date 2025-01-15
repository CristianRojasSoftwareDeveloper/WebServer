using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de agregar un nuevo permiso.
    /// </summary>
    public class AddPermission_CommandHandler : ISyncOperationHandler<AddPermission_Command, Permission>, IAsyncOperationHandler<AddPermission_Command, Permission> {

        private IPermissionRepository _permissionRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de agregar un nuevo permiso.
        /// </summary>
        /// <param name="permissionRepository">Repositorio de permisos.</param>
        public AddPermission_CommandHandler (IPermissionRepository permissionRepository) =>
            _permissionRepository = permissionRepository;

        /// <summary>
        /// Maneja de forma síncrona el comando para agregar un nuevo permiso.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo permiso.</param>
        /// <returns>El permiso agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el permiso son nulos.</exception>
        /// <exception cref="AggregateError">Se lanza si hay errores de validación.</exception>
        public Permission Handle (AddPermission_Command command) {
            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el permiso es nulo
            if (command.Permission == null)
                throw BadRequestError.Create("El permiso no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el nombre del permiso es nulo o vacío
            if (string.IsNullOrWhiteSpace(command.Permission.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.Permission.Name), "El nombre del permiso no puede ser nulo o vacío"));
            else if (_permissionRepository.FirstOrDefault(permission => permission.Name!.Equals(command.Permission.Name)) != null)
                validationErrors.Add(ValidationError.Create(nameof(command.Permission.Name), $"El nombre del permiso '{command.Permission.Name}' ya existe"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Agregar el permiso y devolverlo
            return _permissionRepository.AddPermission(command.Permission);
        }

        /// <summary>
        /// Maneja de forma asíncrona el comando para agregar un nuevo permiso.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el permiso agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el permiso son nulos.</exception>
        /// <exception cref="AggregateError">Se lanza si hay errores de validación.</exception>
        public async Task<Permission> HandleAsync (AddPermission_Command command) {
            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el permiso es nulo
            if (command.Permission == null)
                throw BadRequestError.Create("El permiso no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el nombre del permiso es nulo o vacío
            if (string.IsNullOrWhiteSpace(command.Permission.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.Permission.Name), "El nombre del permiso no puede ser nulo o vacío"));
            else if ((await _permissionRepository.FirstOrDefaultAsync(permission => permission.Name!.Equals(command.Permission.Name))) != null)
                validationErrors.Add(ValidationError.Create(nameof(command.Permission.Name), $"El nombre del permiso '{command.Permission.Name}' ya existe"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Agregar el permiso de forma asíncrona y devolverlo
            return await _permissionRepository.AddPermissionAsync(command.Permission);
        }

    }

}