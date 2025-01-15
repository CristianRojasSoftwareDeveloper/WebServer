using SharedKernel.Domain.Models.Abstractions;

namespace SharedKernel.Domain.Models.Entities.Users.Authorizations {

    /// <summary>
    /// Entidad que representa un rol en el sistema.
    /// Contiene información sobre el rol y sus relaciones con usuarios y permisos.
    /// </summary>
    public class Role : GenericEntity {

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descripción del rol de usuario.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Colección de relaciones entre usuarios y roles.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para modificar usuarios, use los métodos específicos de la clase.
        /// </summary>
        public ICollection<RoleAssignedToUser> RoleAssignedToUsers { get; private set; } = [];

        /// <summary>
        /// Colección de relaciones entre roles y permisos.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para modificar permisos, use los métodos específicos de la clase.
        /// </summary>
        public ICollection<PermissionAssignedToRole> PermissionAssignedToRoles { get; private set; } = [];

    }

}