using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Users.Operations.Use_Cases.Commands.AddRoleToUser {

    /// <summary>
    /// Manejador para el comando de agregar un rol a un usuario.
    /// </summary>
    public class AddRoleToUser_CommandHandler : IOperationHandler<IAddRoleToUser_Command, RoleAssignedToUser> {

        private IUserRepository _userRepository { get; }
        private IRoleRepository _roleRepository { get; }
        private IRoleAssignedToUserRepository _roleAssignedToUserRepository { get; }

        public AddRoleToUser_CommandHandler (IUserRepository userRepository, IRoleRepository roleRepository, IRoleAssignedToUserRepository roleAssignedToUserRepository) {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleAssignedToUserRepository = roleAssignedToUserRepository;
        }

        public async Task<RoleAssignedToUser> Handle (IAddRoleToUser_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verifica si el identificador del usuario existe
            if (command.UserID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.UserID), "El identificador del usuario no es válido"));
            else if ((await _userRepository.GetUserByID(command.UserID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.UserID), $"No se ha encontrado ningún usuario con el identificador {command.UserID}."));

            // Verificar si el identificador del rol existe
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol no es válido"));
            else if ((await _roleRepository.GetRoleByID(command.RoleID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), $"No se ha encontrado ningún rol de usuario con el identificador {command.RoleID}."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            var newRoleAssignedToUser = new RoleAssignedToUser(
                identifier: null,
                userID: command.UserID,
                roleID: command.RoleID
            );

            return await _roleAssignedToUserRepository.AddRoleAssignedToUser(newRoleAssignedToUser);
        }

    }

}