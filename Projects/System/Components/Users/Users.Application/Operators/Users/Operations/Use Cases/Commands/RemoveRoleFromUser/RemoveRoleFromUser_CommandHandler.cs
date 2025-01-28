using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;

namespace Users.Application.Operators.Users.Operations.Use_Cases.Commands.RemoveRoleFromUser {

    /// <summary>
    /// Manejador para el comando de eliminar un rol de un usuario.
    /// </summary>
    public class RemoveRoleFromUser_CommandHandler : IOperationHandler<IRemoveRoleFromUser_Command, bool> {

        private IRoleAssignedToUserRepository _roleAssignedToUserRepository { get; }

        public RemoveRoleFromUser_CommandHandler (IRoleAssignedToUserRepository roleAssignedToUserRepository) =>
            _roleAssignedToUserRepository = roleAssignedToUserRepository;

        public async Task<bool> Handle (IRemoveRoleFromUser_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del usuario es válido
            if (command.UserID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.UserID), "El identificador del usuario no es válido"));

            // Verificar si el identificador del rol de usuario es válido
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol de usuario no es válido"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);
            else {
                var roleAssignedToUser = await _roleAssignedToUserRepository.GetRoleAssignedToUserByForeignKeys(command.UserID, command.RoleID);
                if (roleAssignedToUser == null)
                    throw NotFoundError.Create("RolesAssignedToUser");
                else
                    return await _roleAssignedToUserRepository.DeleteRoleAssignedToUserByID((int) roleAssignedToUser.ID!);
            }

        }

    }

}