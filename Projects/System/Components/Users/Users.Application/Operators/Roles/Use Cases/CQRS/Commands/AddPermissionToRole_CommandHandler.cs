using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Commands;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de agregar un permiso a un rol.
    /// </summary>
    public class AddPermissionToRole_CommandHandler : ISyncOperationHandler<AddPermissionToRole_Command, PermissionAssignedToRole>, IAsyncOperationHandler<AddPermissionToRole_Command, PermissionAssignedToRole> {

        private IRoleRepository _roleRepository { get; }
        private IPermissionRepository _permissionRepository { get; }
        private IPermissionsAssignedToRoleRepository _permissionAssignedToRoleRepository { get; }

        /// <summary>
        /// Inicializa una nueva instancia del manejador del comando para agregar un permiso a un rol.
        /// </summary>
        /// <param name="roleRepository">El repositorio de roles.</param>
        /// <param name="permissionRepository">El repositorio de permisos.</param>
        /// <param name="permissionAssignedToRoleRepository">El repositorio de permisos de roles.</param>
        public AddPermissionToRole_CommandHandler (IRoleRepository roleRepository, IPermissionRepository permissionRepository, IPermissionsAssignedToRoleRepository permissionAssignedToRoleRepository) {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _permissionAssignedToRoleRepository = permissionAssignedToRoleRepository;
        }

        /// <summary>
        /// Maneja el comando para agregar un permiso a un rol de forma síncrona.
        /// </summary>
        /// <param name="command">El comando para agregar el permiso.</param>
        /// <returns>El permiso de rol agregado.</returns>
        public PermissionAssignedToRole Handle (AddPermissionToRole_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del rol es válido y existe en el repositorio de roles
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol de usuario no es válido"));
            else if (_roleRepository.GetRoleByID(command.RoleID) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), $"No se ha encontrado ningún rol de usuario con el identificador {command.RoleID}."));

            // Verificar si el identificador del permiso es válido y existe en el repositorio de permisos
            if (command.PermissionID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), "El identificador del permiso de usuario no es válido"));
            else if (_permissionRepository.GetPermissionByID(command.PermissionID) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), $"No se ha encontrado ningún permiso de usuario con el identificador {command.PermissionID}."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Crear el nuevo permiso de rol y guardarlo en el repositorio
            var newPermissionAssignedToRole = new PermissionAssignedToRole {
                RoleID = command.RoleID,
                PermissionID = command.PermissionID
            };
            return _permissionAssignedToRoleRepository.AddPermissionAssignedToRole(newPermissionAssignedToRole);

        }

        /// <summary>
        /// Maneja el comando para agregar un permiso a un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando para agregar el permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el permiso de rol agregado.</returns>
        public async Task<PermissionAssignedToRole> HandleAsync (AddPermissionToRole_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del rol es válido y existe en el repositorio de roles
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol de usuario no es válido"));
            else if ((await _roleRepository.GetRoleByIDAsync(command.RoleID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), $"No se ha encontrado ningún rol de usuario con el identificador {command.RoleID}."));

            // Verificar si el identificador del permiso es válido y existe en el repositorio de permisos
            if (command.PermissionID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), "El identificador del permiso de usuario no es válido"));
            else if ((await _permissionRepository.GetPermissionByIDAsync(command.PermissionID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), $"No se ha encontrado ningún permiso de usuario con el identificador {command.PermissionID}."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Crear el nuevo permiso de rol y guardarlo en el repositorio
            var newPermissionAssignedToRole = new PermissionAssignedToRole {
                RoleID = command.RoleID,
                PermissionID = command.PermissionID
            };
            return await _permissionAssignedToRoleRepository.AddPermissionAssignedToRoleAsync(newPermissionAssignedToRole);

        }

    }

}