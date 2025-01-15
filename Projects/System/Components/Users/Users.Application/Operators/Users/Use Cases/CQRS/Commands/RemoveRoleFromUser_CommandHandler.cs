using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Commands;

namespace Users.Application.Operators.Users.UseCases.CQRS.Commands {

    /// <summary>
    /// Manejador para el comando de eliminar un rol de un usuario.
    /// </summary>
    public class RemoveRoleFromUser_CommandHandler : ISyncOperationHandler<RemoveRoleFromUser_Command, bool>, IAsyncOperationHandler<RemoveRoleFromUser_Command, bool> {

        private IRoleAssignedToUserRepository _roleAssignedToUserRepository { get; }

        public RemoveRoleFromUser_CommandHandler (IRoleAssignedToUserRepository roleAssignedToUserRepository) =>
            _roleAssignedToUserRepository = roleAssignedToUserRepository;

        public bool Handle (RemoveRoleFromUser_Command command) {

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
                var roleAssignedToUser = _roleAssignedToUserRepository.GetRoleAssignedToUserByForeignKeys(command.UserID, command.RoleID);
                if (roleAssignedToUser == null)
                    throw NotFoundError.Create("RolesAssignedToUser");
                else
                    return _roleAssignedToUserRepository.DeleteRoleAssignedToUserByID((int) roleAssignedToUser.ID!);
            }

        }

        public async Task<bool> HandleAsync (RemoveRoleFromUser_Command command) {

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
                var roleAssignedToUser = await _roleAssignedToUserRepository.GetRoleAssignedToUserByForeignKeysAsync(command.UserID, command.RoleID);
                if (roleAssignedToUser == null)
                    throw NotFoundError.Create("RolesAssignedToUser");
                else
                    return await _roleAssignedToUserRepository.DeleteRoleAssignedToUserByIDAsync((int) roleAssignedToUser.ID!);
            }

        }

    }

}