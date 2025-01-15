using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Queries;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators {

    /// <summary>
    /// Interfaz para operaciones relacionadas con la gestión de roles de usuario.
    /// </summary>
    public interface IRoleOperator : IReflexiveOperator {

        #region Métodos síncronos

        /// <summary>
        /// Agrega un rol de forma síncrona.
        /// </summary>
        /// <param name="command">El comando con la información del nuevo rol a agregar.</param>
        /// <returns>Un resultado que contiene el rol agregado o un Error si la operación falla.</returns>
        Response<Role> AddRole (AddRole_Command command);

        /// <summary>
        /// Obtiene todos los roles de forma síncrona.
        /// </summary>
        /// <param name="query">El query para obtener todos los roles.</param>
        /// <returns>Un resultado que contiene una colección de todos los roles o un Error si la operación falla.</returns>
        Response<List<Role>> GetRoles (GetRoles_Query query);

        /// <summary>
        /// Obtiene un rol por su ID de forma síncrona.
        /// </summary>
        /// <param name="query">El query con el ID del rol a obtener.</param>
        /// <returns>Un resultado que contiene el rol obtenido o un Error si no se encuentra.</returns>
        Response<Role> GetRoleByID (GetRoleByID_Query query);

        /// <summary>
        /// Obtiene los roles asociados a un usuario específico de forma síncrona.
        /// </summary>
        /// <param name="query">El query con el ID del usuario cuyas roles se desean obtener.</param>
        /// <returns>Un resultado que contiene una colección de roles asociados al usuario o un Error si la operación falla.</returns>
        Response<List<Role>> GetRolesByUserID (GetRolesByUserID_Query query);

        /// <summary>
        /// Actualiza un rol existente de forma síncrona.
        /// </summary>
        /// <param name="command">El comando con los datos actualizados del rol.</param>
        /// <returns>Un resultado que contiene el rol actualizado o un Error si la operación falla.</returns>
        Response<Role> UpdateRole (UpdateRole_Command command);

        /// <summary>
        /// Elimina un rol por su ID de forma síncrona.
        /// </summary>
        /// <param name="command">El comando con el ID del rol a eliminar.</param>
        /// <returns>Un resultado que indica si el rol fue eliminado exitosamente o un Error si la operación falla.</returns>
        Response<bool> DeleteRoleByID (DeleteRoleByID_Command command);

        /// <summary>
        /// Agrega un permiso a un rol de forma síncrona.
        /// </summary>
        /// <param name="command">El comando con la información del permiso a agregar.</param>
        /// <returns>Un resultado que contiene el permiso agregado al rol o un Error si la operación falla.</returns>
        Response<PermissionAssignedToRole> AddPermissionToRole (AddPermissionToRole_Command command);

        /// <summary>
        /// Elimina un permiso de un rol de forma síncrona.
        /// </summary>
        /// <param name="command">El comando con la información del permiso a eliminar.</param>
        /// <returns>Un resultado que indica si el permiso fue eliminado exitosamente o un Error si la operación falla.</returns>
        Response<bool> RemovePermissionFromRole (RemovePermissionFromRole_Command command);

        #endregion

        #region Métodos asíncronos

        /// <summary>
        /// Agrega un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando con la información del nuevo rol a agregar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el rol agregado o un Error si la operación falla.</returns>
        Task<Response<Role>> AddRoleAsync (AddRole_Command command);

        /// <summary>
        /// Obtiene todos los roles de forma asíncrona.
        /// </summary>
        /// <param name="query">El query para obtener todos los roles.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de todos los roles o un Error si la operación falla.</returns>
        Task<Response<List<Role>>> GetRolesAsync (GetRoles_Query query);

        /// <summary>
        /// Obtiene un rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">El query con el ID del rol a obtener.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el rol obtenido o un Error si no se encuentra.</returns>
        Task<Response<Role>> GetRoleByIDAsync (GetRoleByID_Query query);

        /// <summary>
        /// Obtiene los roles asociados a un usuario específico de forma asíncrona.
        /// </summary>
        /// <param name="query">El query con el ID del usuario cuyas roles se desean obtener.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de roles asociados al usuario o un Error si la operación falla.</returns>
        Task<Response<List<Role>>> GetRolesByUserIDAsync (GetRolesByUserID_Query query);

        /// <summary>
        /// Actualiza un rol existente de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando con los datos actualizados del rol.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el rol actualizado o un Error si la operación falla.</returns>
        Task<Response<Role>> UpdateRoleAsync (UpdateRole_Command command);

        /// <summary>
        /// Elimina un rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando con el ID del rol a eliminar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si el rol fue eliminado exitosamente o un Error si la operación falla.</returns>
        Task<Response<bool>> DeleteRoleByIDAsync (DeleteRoleByID_Command command);

        /// <summary>
        /// Agrega un permiso a un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando con la información del permiso a agregar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el permiso agregado al rol o un Error si la operación falla.</returns>
        Task<Response<PermissionAssignedToRole>> AddPermissionToRoleAsync (AddPermissionToRole_Command command);

        /// <summary>
        /// Elimina un permiso de un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando con la información del permiso a eliminar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si el permiso fue eliminado exitosamente o un Error si la operación falla.</returns>
        Task<Response<bool>> RemovePermissionFromRoleAsync (RemovePermissionFromRole_Command command);

        #endregion

    }

}