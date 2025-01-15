using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Commands {

    /// <summary>
    /// Comando para agregar un nuevo rol.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.AddRole, Enumerations.Permissions.AddEntity])]
    public class AddRole_Command : Operation {

        /// <summary>
        /// Obtiene el rol que se va a agregar.
        /// </summary>
        public Role Role { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el rol especificado.
        /// </summary>
        /// <param name="role">El rol a agregar.</param>
        public AddRole_Command (Role role) => Role = role;

    }

}
