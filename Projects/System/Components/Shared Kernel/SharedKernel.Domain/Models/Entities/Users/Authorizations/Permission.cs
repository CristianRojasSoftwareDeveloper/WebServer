using SharedKernel.Domain.Models.Abstractions;

namespace SharedKernel.Domain.Models.Entities.Users.Authorizations {

    /// <summary>
    /// Entidad que representa un permiso en el sistema.
    /// Contiene información sobre el permiso y sus relaciones con roles.
    /// </summary>
    public class Permission : GenericEntity {

        /// <summary>
        /// Nombre del permiso.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descripción del permiso de usuario.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Colección de relaciones entre roles y permisos.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para modificar roles, use los métodos específicos de la clase.
        /// </summary>
        public ICollection<PermissionAssignedToRole> PermissionAssignedToRoles { get; private set; } = [];

    }

}