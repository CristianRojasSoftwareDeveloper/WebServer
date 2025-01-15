using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Commands;

namespace Users.Application.Operators.Roles.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de eliminación de un permiso de un rol.
    /// </summary>
    public class RemovePermissionFromRole_CommandHandler : ISyncOperationHandler<RemovePermissionFromRole_Command, bool>, IAsyncOperationHandler<RemovePermissionFromRole_Command, bool> {

        private IPermissionsAssignedToRoleRepository _permissionAssignedToRoleRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de eliminación de permisos de roles.
        /// </summary>
        /// <param name="permissionAssignedToRoleRepository">El repositorio de permisos de roles.</param>
        public RemovePermissionFromRole_CommandHandler (IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository) =>
            _permissionAssignedToRoleRepository = permissionAssignedToRoleRepository;

        /// <summary>
        /// Maneja el comando para eliminar un permiso de un rol de forma síncrona.
        /// </summary>
        /// <param name="command">El comando para eliminar el permiso.</param>
        /// <returns>True si se elimina correctamente, de lo contrario false.</returns>
        public bool Handle (RemovePermissionFromRole_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del rol es válido y existe en el repositorio de roles
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol de usuario no es válido"));

            // Verificar si el identificador del permiso es válido y existe en el repositorio de permisos
            if (command.PermissionID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), "El identificador del permiso de usuario no es válido"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);
            else {
                var permissionAssignedToRole = _permissionAssignedToRoleRepository.GetPermissionAssignedToRoleByForeignKeys(command.RoleID, command.PermissionID);
                if (permissionAssignedToRole == null)
                    throw NotFoundError.Create("PermissionAssignedToRole");
                else
                    return _permissionAssignedToRoleRepository.DeletePermissionAssignedToRoleByID((int) permissionAssignedToRole.ID!);
            }

        }

        /// <summary>
        /// Maneja el comando para eliminar un permiso de un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando para eliminar el permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un valor booleano que indica si se eliminó correctamente.</returns>
        public async Task<bool> HandleAsync (RemovePermissionFromRole_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del rol es válido y existe en el repositorio de roles
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol de usuario no es válido"));

            // Verificar si el identificador del permiso es válido y existe en el repositorio de permisos
            if (command.PermissionID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), "El identificador del permiso de usuario no es válido"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);
            else {
                var permissionAssignedToRole = await _permissionAssignedToRoleRepository.GetPermissionAssignedToRoleByForeignKeysAsync(command.RoleID, command.PermissionID);
                if (permissionAssignedToRole == null)
                    throw NotFoundError.Create("PermissionAssignedToRole");
                else
                    return await _permissionAssignedToRoleRepository.DeletePermissionAssignedToRoleByIDAsync((int) permissionAssignedToRole.ID!);
            }

        }

    }

}