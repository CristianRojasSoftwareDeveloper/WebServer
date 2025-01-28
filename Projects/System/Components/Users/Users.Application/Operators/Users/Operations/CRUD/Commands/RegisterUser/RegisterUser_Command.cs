using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser {

    /// <summary>
    /// Comando para registrar un nuevo usuario.
    /// </summary>
    [RequiredPermissions([SystemPermissions.RegisterUser, SystemPermissions.AddEntity])]
    [AssociatedOperator(typeof(IUserOperator))]
    public class RegisterUser_Command : IRegisterUser_Command {

        /// <summary>
        /// Obtiene el usuario que se va a registrar.
        /// </summary>
        public User Entity { get; }

        public int[] AssociatedRolesIdentifiers { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando de registro de usuario.
        /// </summary>
        /// <param name="user">El usuario que se va a registrar.</param>
        public RegisterUser_Command (User user, params int[] associatedRoleIdentifiers) {
            Entity = user;
            // Si no se proporcionan identificadores de roles asociados, se asigna el rol por defecto "Entity", el cual tiene el identificador número 3 [ID = 3].
            AssociatedRolesIdentifiers = associatedRoleIdentifiers.Length > 0 ? associatedRoleIdentifiers : [3];
        }

    }

}