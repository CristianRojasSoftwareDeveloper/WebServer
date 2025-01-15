using SharedKernel.Domain.Models.Abstractions;

namespace SharedKernel.Domain.Models.Entities.Users.Authorizations {

    /// <summary>
    /// Entidad que representa la relación entre usuarios y roles.
    /// </summary>
    public class RoleAssignedToUser : GenericEntity {

        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Usuario asociado a esta relación.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para establecer la relación, use la propiedad UserID.
        /// </summary>
        public User? User { get; private set; }

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
    }
}