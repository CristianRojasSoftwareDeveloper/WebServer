using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Commands {

    /// <summary>
    /// Comando para actualizar un permiso.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.UpdatePermission, Enumerations.Permissions.UpdateEntity])]
    public class UpdatePermission_Command : Operation {

        /// <summary>
        /// Obtiene el permiso que se va a actualizar.
        /// </summary>
        public Permission Permission { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el permiso especificado.
        /// </summary>
        /// <param name="permission">El permiso a actualizar.</param>
        public UpdatePermission_Command (Permission permission) {
            Permission = permission;
        }

    }

}