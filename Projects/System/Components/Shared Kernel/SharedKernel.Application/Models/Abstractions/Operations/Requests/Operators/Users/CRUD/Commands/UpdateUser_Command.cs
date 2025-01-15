using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Commands {

    /// <summary>
    /// Comando para actualizar un usuario existente.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.UpdateUser, Enumerations.Permissions.UpdateEntity])]
    public class UpdateUser_Command : Operation {

        /// <summary>
        /// Obtiene el usuario que se va a actualizar.
        /// </summary>
        public User User { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para actualizar un usuario.
        /// </summary>
        /// <param name="user">El usuario con los datos actualizados.</param>
        public UpdateUser_Command (User user) => User = user;

    }

}