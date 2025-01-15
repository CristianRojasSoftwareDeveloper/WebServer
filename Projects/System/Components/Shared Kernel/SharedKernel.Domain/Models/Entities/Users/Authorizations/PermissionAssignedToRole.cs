using SharedKernel.Domain.Models.Abstractions;

namespace SharedKernel.Domain.Models.Entities.Users.Authorizations {

    /// <summary>
    /// Entidad que representa la relación entre roles y permisos.
    /// </summary>
    public class PermissionAssignedToRole : GenericEntity {

        /// <summary>
        /// Identificador del rol.
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Rol asociado a esta relación.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para establecer la relación, use la propiedad RoleID.
        /// </summary>
        public Role? Role { get; private set; }

        /// <summary>
        /// Identificador del permiso.
        /// </summary>
        public int PermissionID { get; set; }

        /// <summary>
        /// Permiso asociado a esta relación.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para establecer la relación, use la propiedad PermissionID.
        /// </summary>
        public Permission? Permission { get; private set; }

    }

}