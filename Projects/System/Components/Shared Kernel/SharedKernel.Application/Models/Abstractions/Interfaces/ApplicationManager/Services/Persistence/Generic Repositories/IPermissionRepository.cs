using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories {

    public interface IPermissionRepository : IQueryableRepository<Permission> {

        #region Métodos síncronos específicos de permiso de usuario

        /// <summary>
        /// Agrega un nuevo permiso de usuario al repositorio.
        /// </summary>
        /// <param name="newPermission">El permiso de usuario a agregar.</param>
        /// <returns>El permiso de usuario agregado.</returns>
        Permission AddPermission (Permission newPermission);

        /// <summary>
        /// Obtiene todos los permiso de usuarios del repositorio.
        /// </summary>
        /// <returns>Una colección de permiso de usuarios.</returns>
        List<Permission> GetPermissions ();

        /// <summary>
        /// Obtiene un permiso de usuario por su ID.
        /// </summary>
        /// <param name="permissionID">El ID del permiso de usuario.</param>
        /// <returns>El permiso de usuario encontrado o null si no existe.</returns>
        Permission GetPermissionByID (int permissionID);

        /// <summary>
        /// Actualiza un permiso de usuario existente por su ID.
        /// </summary>
        /// <param name="updatedPermission">El permiso de usuario actualizado.</param>
        /// <returns>El permiso de usuario actualizado o null si no se encontró.</returns>
        Permission UpdatePermission (Permission updatedPermission);

        /// <summary>
        /// Elimina un permiso de usuario por su ID.
        /// </summary>
        /// <param name="permissionID">El ID del permiso de usuario a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, false si no se encontró el permiso de usuario.</returns>
        bool DeletePermissionByID (int permissionID);

        #endregion

        #region Métodos asíncronos específicos de permiso de usuario

        /// <summary>
        /// Agrega un nuevo permiso de usuario al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newPermission">El permiso de usuario a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de usuario agregado como resultado.</returns>
        Task<Permission> AddPermissionAsync (Permission newPermission);

        /// <summary>
        /// Obtiene todos los permiso de usuarios del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de permiso de usuarios como resultado.</returns>
        Task<List<Permission>> GetPermissionsAsync ();

        /// <summary>
        /// Obtiene un permiso de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionID">El ID del permiso de usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de usuario encontrado o null si no existe como resultado.</returns>
        Task<Permission> GetPermissionByIDAsync (int permissionID);

        /// <summary>
        /// Actualiza un permiso de usuario existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="updatedPermission">El permiso de usuario actualizado.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de usuario actualizado o null si no se encontró como resultado.</returns>
        Task<Permission> UpdatePermissionAsync (Permission updatedPermission);

        /// <summary>
        /// Elimina un permiso de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionID">El ID del permiso de usuario a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<bool> DeletePermissionByIDAsync (int permissionID);

        #endregion

    }

}