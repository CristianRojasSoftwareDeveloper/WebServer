using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Commands {

    /// <summary>
    /// Comando para actualizar un rol.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.UpdateRole, Enumerations.Permissions.UpdateEntity])]
    public class UpdateRole_Command : Operation {

        /// <summary>
        /// Obtiene el rol que se va a actualizar.
        /// </summary>
        public Role Role { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el rol especificado.
        /// </summary>
        /// <param name="role">El rol a actualizar.</param>
        public UpdateRole_Command (Role role) {
            Role = role;
        }

    }

}