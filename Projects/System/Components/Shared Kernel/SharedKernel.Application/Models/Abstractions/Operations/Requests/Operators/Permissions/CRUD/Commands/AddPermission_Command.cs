using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Commands {

    /// <summary>
    /// Comando para agregar un nuevo permiso.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.AddPermission, Enumerations.Permissions.AddEntity])]
    public class AddPermission_Command : Operation {

        /// <summary>
        /// Obtiene el permiso que se va a agregar.
        /// </summary>
        public Permission Permission { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el permiso especificado.
        /// </summary>
        /// <param name="permission">El permiso a agregar.</param>
        public AddPermission_Command (Permission permission) {
            Permission = permission;
        }

    }

}