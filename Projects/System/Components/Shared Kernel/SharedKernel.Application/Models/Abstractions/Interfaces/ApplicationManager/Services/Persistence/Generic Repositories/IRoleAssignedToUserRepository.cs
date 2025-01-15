using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories {

    public interface IRoleAssignedToUserRepository : IQueryableRepository<RoleAssignedToUser> {

        #region Métodos síncronos específicos de rol de usuario

        /// <summary>
        /// Agrega un nuevo rol de usuario al repositorio.
        /// </summary>
        /// <param name="newRoleAssignedToUser">El rol de usuario a agregar.</param>
        /// <returns>El rol de usuario agregado.</returns>
        RoleAssignedToUser AddRoleAssignedToUser (RoleAssignedToUser newRoleAssignedToUser);

        /// <summary>
        /// Obtiene un rol de usuario por su ID.
        /// </summary>
        /// <param name="roleAssignedToUserID">El ID del rol de usuario.</param>
        /// <returns>El rol de usuario encontrado o null si no existe.</returns>
        RoleAssignedToUser GetRoleAssignedToUserByID (int roleAssignedToUserID);

        RoleAssignedToUser GetRoleAssignedToUserByForeignKeys (int userID, int roleID);

        /// <summary>
        /// Obtiene todos los rol de usuarios del repositorio.
        /// </summary>
        /// <returns>Una colección de rol de usuarios.</returns>
        List<RoleAssignedToUser> GetRolesAssignedToUsers ();

        /// <summary>
        /// Obtiene todos los roles de usuario asociados a un usuario según su ID.
        /// </summary>
        /// <returns>Una colección de rol de usuarios.</returns>
        List<RoleAssignedToUser> GetRolesAssignedToUserByUserID (int userID);

        /// <summary>
        /// Actualiza un rol de usuario existente por su ID.
        /// </summary>
        /// <param name="updatedRoleAssignedToUser">El rol de usuario actualizado.</param>
        /// <returns>El rol de usuario actualizado o null si no se encontró.</returns>
        RoleAssignedToUser UpdateRoleAssignedToUser (RoleAssignedToUser updatedRoleAssignedToUser);

        /// <summary>
        /// Elimina un rol de usuario por su ID.
        /// </summary>
        /// <param name="roleAssignedToUserID">El ID del rol de usuario a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, false si no se encontró el rol de usuario.</returns>
        bool DeleteRoleAssignedToUserByID (int roleAssignedToUserID);

        #endregion

        #region Métodos asíncronos específicos de rol de usuario

        /// <summary>
        /// Agrega un nuevo rol de usuario al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newRoleAssignedToUser">El rol de usuario a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol de usuario agregado como resultado.</returns>
        Task<RoleAssignedToUser> AddRoleAssignedToUserAsync (RoleAssignedToUser newRoleAssignedToUser);

        /// <summary>
        /// Obtiene un rol de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleAssignedToUserID">El ID del rol de usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol de usuario encontrado o null si no existe como resultado.</returns>
        Task<RoleAssignedToUser> GetRoleAssignedToUserByIDAsync (int roleAssignedToUserID);

        Task<RoleAssignedToUser> GetRoleAssignedToUserByForeignKeysAsync (int userID, int roleID);

        /// <summary>
        /// Obtiene todos los rol de usuarios del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de rol de usuarios como resultado.</returns>
        Task<List<RoleAssignedToUser>> GetRolesAssignedToUsersAsync ();

        /// <summary>
        /// Obtiene todos los roles de usuario asociados a un usuario según su ID.
        /// </summary>
        /// <returns>Una colección de rol de usuarios.</returns>
        Task<List<RoleAssignedToUser>> GetRolesAssignedToUserByUserIDAsync (int userID);

        /// <summary>
        /// Actualiza un rol de usuario existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="updatedRoleAssignedToUser">El rol de usuario actualizado.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol de usuario actualizado o null si no se encontró como resultado.</returns>
        Task<RoleAssignedToUser> UpdateRoleAssignedToUserAsync (RoleAssignedToUser updatedRoleAssignedToUser);

        /// <summary>
        /// Elimina un rol de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleAssignedToUserID">El ID del rol de usuario a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<bool> DeleteRoleAssignedToUserByIDAsync (int ID_roleAssignedToUser);

        #endregion

    }

}