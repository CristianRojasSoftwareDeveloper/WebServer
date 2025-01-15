using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories {

    public interface IPermissionsAssignedToRoleRepository : IQueryableRepository<PermissionAssignedToRole> {

        #region Métodos síncronos específicos de permiso de rol

        /// <summary>
        /// Agrega un nuevo permiso de rol al repositorio.
        /// </summary>
        /// <param name="newPermissionAssignedToRole">El permiso de rol a agregar.</param>
        /// <returns>El permiso de rol agregado.</returns>
        PermissionAssignedToRole AddPermissionAssignedToRole (PermissionAssignedToRole newPermissionAssignedToRole);

        /// <summary>
        /// Obtiene todos los permiso de roles del repositorio.
        /// </summary>
        /// <returns>Una colección de permiso de roles.</returns>
        List<PermissionAssignedToRole> GetPermissionAssignedToRoles ();

        /// <summary>
        /// Obtiene todos los permisos de usuario asociados a un rol según su ID.
        /// </summary>
        /// <returns>Una colección de permisos del rol.</returns>
        List<PermissionAssignedToRole> GetPermissionAssignedToRolesByRoleID (int roleID);

        /// <summary>
        /// Obtiene un permiso de rol por su ID.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">El ID del permiso de rol.</param>
        /// <returns>El permiso de rol encontrado o null si no existe.</returns>
        PermissionAssignedToRole GetPermissionAssignedToRoleByID (int permissionAssignedToRoleID);

        PermissionAssignedToRole GetPermissionAssignedToRoleByForeignKeys (int roleID, int permissionID);

        /// <summary>
        /// Actualiza un permiso de rol existente por su ID.
        /// </summary>
        /// <param name="updatedPermissionAssignedToRole">El permiso de rol actualizado.</param>
        /// <returns>El permiso de rol actualizado o null si no se encontró.</returns>
        PermissionAssignedToRole UpdatePermissionAssignedToRole (PermissionAssignedToRole updatedPermissionAssignedToRole);

        /// <summary>
        /// Elimina un permiso de rol por su ID.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">El ID del permiso de rol a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, false si no se encontró el permiso de rol.</returns>
        bool DeletePermissionAssignedToRoleByID (int permissionAssignedToRoleID);

        #endregion

        #region Métodos asíncronos específicos de permiso de rol

        /// <summary>
        /// Agrega un nuevo permiso de rol al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newPermissionAssignedToRole">El permiso de rol a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de rol agregado como resultado.</returns>
        Task<PermissionAssignedToRole> AddPermissionAssignedToRoleAsync (PermissionAssignedToRole newPermissionAssignedToRole);

        /// <summary>
        /// Obtiene todos los permiso de roles del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de permiso de roles como resultado.</returns>
        Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRolesAsync ();

        /// <summary>
        /// Obtiene todos los permisos de usuario asociados a un rol según su ID.
        /// </summary>
        /// <returns>Una colección de permisos del rol.</returns>
        Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRolesByRoleIDAsync (int roleID);

        /// <summary>
        /// Obtiene un permiso de rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">El ID del permiso de rol.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de rol encontrado o null si no existe como resultado.</returns>
        Task<PermissionAssignedToRole> GetPermissionAssignedToRoleByIDAsync (int permissionAssignedToRoleID);

        Task<PermissionAssignedToRole> GetPermissionAssignedToRoleByForeignKeysAsync (int roleID, int permissionID);

        /// <summary>
        /// Actualiza un permiso de rol existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="updatedPermissionAssignedToRole">El permiso de rol actualizado.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de rol actualizado o null si no se encontró como resultado.</returns>
        Task<PermissionAssignedToRole> UpdatePermissionAssignedToRoleAsync (PermissionAssignedToRole updatedPermissionAssignedToRole);

        /// <summary>
        /// Elimina un permiso de rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">El ID del permiso de rol a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<bool> DeletePermissionAssignedToRoleByIDAsync (int permissionAssignedToRoleID);

        #endregion

    }

}