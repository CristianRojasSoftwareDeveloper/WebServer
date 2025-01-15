using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Commands {

    /// <summary>
    /// Comando para registrar un nuevo usuario.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.RegisterUser, Enumerations.Permissions.AddEntity])]
    [AssociatedOperator(typeof(IUserOperator))]
    public class RegisterUser_Command : Operation {

        /// <summary>
        /// Obtiene el usuario que se va a registrar.
        /// </summary>
        public User User { get; }

        public int[] AssociatedRolesIdentifiers { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando de registro de usuario.
        /// </summary>
        /// <param name="user">El usuario que se va a registrar.</param>
        public RegisterUser_Command (User user, params int[] associatedRoleIdentifiers) {
            User = user;
            // Si no se proporcionan identificadores de roles asociados, se asigna el rol por defecto "User", el cual tiene el identificador número 3 [ID = 3].
            AssociatedRolesIdentifiers = associatedRoleIdentifiers.Length > 0 ? associatedRoleIdentifiers : [3];
        }

    }

}