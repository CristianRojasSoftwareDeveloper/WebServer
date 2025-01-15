using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de actualización de permiso.
    /// </summary>
    public class UpdatePermission_CommandHandler : ISyncOperationHandler<UpdatePermission_Command, Permission>, IAsyncOperationHandler<UpdatePermission_Command, Permission> {

        private IPermissionRepository _permissionRepository { get; }

        public UpdatePermission_CommandHandler (IPermissionRepository permissionRepository) =>
            _permissionRepository = permissionRepository;

        /// <summary>
        /// Maneja el comando de actualización de un permiso de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el permiso a actualizar.</param>
        /// <returns>El permiso actualizado.</returns>
        public Permission Handle (UpdatePermission_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el permiso es nulo
            if (command.Permission == null)
                throw BadRequestError.Create("El permiso de usuario no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del permiso es válido
            if (command.Permission.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.Permission.ID), "El identificador del permiso de usuario no es válido"));

            // Verificar si el nombre del permiso no está vacío
            if (command.Permission.Name != null)
                if (string.IsNullOrWhiteSpace(command.Permission.Name))
                    validationErrors.Add(ValidationError.Create(nameof(command.Permission.Name), "El nombre del permiso de usuario no puede ser vacío"));
                else if (_permissionRepository.FirstOrDefault(permission => permission.Name!.Equals(command.Permission.Name)) != null)
                    validationErrors.Add(ValidationError.Create(nameof(command.Permission.Name), $"El nombre del permiso de usuario '{command.Permission.Name}' ya existe"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecutar la actualización del permiso
            return _permissionRepository.UpdatePermission(command.Permission);

        }

        /// <summary>
        /// Maneja el comando de actualización de un permiso de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el permiso a actualizar.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene el permiso actualizado.</returns>
        public async Task<Permission> HandleAsync (UpdatePermission_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el permiso es nulo
            if (command.Permission == null)
                throw BadRequestError.Create("El permiso no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del permiso es válido
            if (command.Permission.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.Permission.ID), "El identificador del permiso de usuario no es válido"));

            // Verificar si el nombre del permiso no está vacío
            if (command.Permission.Name != null)
                if (string.IsNullOrWhiteSpace(command.Permission.Name))
                    validationErrors.Add(ValidationError.Create(nameof(command.Permission.Name), "El nombre del permiso de usuario no puede ser vacío"));
                else if ((await _permissionRepository.FirstOrDefaultAsync(permission => permission.Name!.Equals(command.Permission.Name))) != null)
                    validationErrors.Add(ValidationError.Create(nameof(command.Permission.Name), $"El nombre del permiso de usuario '{command.Permission.Name}' ya existe"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecutar la actualización del permiso de forma asíncrona
            return await _permissionRepository.UpdatePermissionAsync(command.Permission);

        }

    }

}