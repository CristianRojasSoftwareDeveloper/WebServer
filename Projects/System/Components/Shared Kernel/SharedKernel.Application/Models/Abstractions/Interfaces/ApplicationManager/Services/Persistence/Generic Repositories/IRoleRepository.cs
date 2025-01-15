using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories {

    public interface IRoleRepository : IQueryableRepository<Role> {

        #region Métodos síncronos específicos de rol

        /// <summary>
        /// Agrega un nuevo rol al repositorio.
        /// </summary>
        /// <param name="newRole">El rol a agregar.</param>
        /// <returns>El rol agregado.</returns>
        Role AddRole (Role newRole);

        /// <summary>
        /// Obtiene todos los roles del repositorio.
        /// </summary>
        /// <returns>Una colección de roles.</returns>
        List<Role> GetRoles ();

        /// <summary>
        /// Obtiene un rol por su ID.
        /// </summary>
        /// <param name="roleID">El ID del rol.</param>
        /// <returns>El rol encontrado o null si no existe.</returns>
        Role GetRoleByID (int roleID);

        /// <summary>
        /// Actualiza un rol existente por su ID.
        /// </summary>
        /// <param name="updatedRole">El rol actualizado.</param>
        /// <returns>El rol actualizado o null si no se encontró.</returns>
        Role UpdateRole (Role updatedRole);

        /// <summary>
        /// Elimina un rol por su ID.
        /// </summary>
        /// <param name="roleID">El ID del rol a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, false si no se encontró el rol.</returns>
        bool DeleteRoleByID (int roleID);

        #endregion

        #region Métodos asíncronos específicos de rol

        /// <summary>
        /// Agrega un nuevo rol al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newRole">El rol a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol agregado como resultado.</returns>
        Task<Role> AddRoleAsync (Role newRole);

        /// <summary>
        /// Obtiene todos los roles del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de roles como resultado.</returns>
        Task<List<Role>> GetRolesAsync ();

        /// <summary>
        /// Obtiene un rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleID">El ID del rol.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol encontrado o null si no existe como resultado.</returns>
        Task<Role> GetRoleByIDAsync (int roleID);

        /// <summary>
        /// Actualiza un rol existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="updatedRole">El rol actualizado.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol actualizado o null si no se encontró como resultado.</returns>
        Task<Role> UpdateRoleAsync (Role updatedRole);

        /// <summary>
        /// Elimina un rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleID">El ID del rol a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<bool> DeleteRoleByIDAsync (int roleID);

        #endregion

    }

}