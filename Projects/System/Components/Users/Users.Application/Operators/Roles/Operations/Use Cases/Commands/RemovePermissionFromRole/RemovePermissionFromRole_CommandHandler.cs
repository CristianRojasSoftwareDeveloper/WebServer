using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.Use_Cases.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;

namespace Users.Application.Operators.Roles.Operations.Use_Cases.Commands.RemovePermissionFromRole {

    /// <summary>
    /// Manejador para el comando de eliminación de un permiso de un rol.
    /// </summary>
    public class RemovePermissionFromRole_CommandHandler : IOperationHandler<IRemovePermissionFromRole_Command, bool> {

        private IPermissionsAssignedToRoleRepository _permissionAssignedToRoleRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de eliminación de permisos de roles.
        /// </summary>
        /// <param name="permissionAssignedToRoleRepository">El repositorio de permisos de roles.</param>
        public RemovePermissionFromRole_CommandHandler (IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository) =>
            _permissionAssignedToRoleRepository = permissionAssignedToRoleRepository;

        /// <summary>
        /// Maneja el comando para eliminar un permiso de un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando para eliminar el permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un valor booleano que indica si se eliminó exitosamente.</returns>
        public async Task<bool> Handle (IRemovePermissionFromRole_Command command) {

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
                var permissionAssignedToRole = await _permissionAssignedToRoleRepository.GetPermissionAssignedToRoleByForeignKeys(command.RoleID, command.PermissionID);
                if (permissionAssignedToRole == null)
                    throw NotFoundError.Create("PermissionAssignedToRole");
                else
                    return await _permissionAssignedToRoleRepository.DeletePermissionAssignedToRoleByID((int) permissionAssignedToRole.ID!);
            }

        }

    }

}