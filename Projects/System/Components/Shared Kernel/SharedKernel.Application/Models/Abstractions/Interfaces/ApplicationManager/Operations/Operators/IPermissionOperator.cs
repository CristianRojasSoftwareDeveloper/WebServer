using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Permissions.UseCases.Queries;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators {

    /// <summary>
    /// Interfaz para operaciones relacionadas con la gestión de los permisos de usuario.
    /// </summary>
    public interface IPermissionOperator : IReflexiveOperator {

        #region Métodos síncronos

        /// <summary>
        /// Agrega un permiso de forma síncrona.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo permiso.</param>
        /// <returns>Un resultado que contiene el permiso agregado o un Error si la operación falla.</returns>
        Response<Permission> AddPermission (AddPermission_Command command);

        /// <summary>
        /// Obtiene todos los permisos de forma síncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener todos los permisos.</param>
        /// <returns>Un resultado que contiene una colección de todos los permisos o un Error si la operación falla.</returns>
        Response<List<Permission>> GetPermissions (GetPermissions_Query query);

        /// <summary>
        /// Obtiene un permiso por su ID de forma síncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener un permiso por su ID.</param>
        /// <returns>Un resultado que contiene el permiso obtenido o un Error si no se encuentra.</returns>
        Response<Permission> GetPermissionByID (GetPermissionByID_Query query);

        /// <summary>
        /// Obtiene los permisos asociados a un rol de forma síncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener los permisos asociados a un rol.</param>
        /// <returns>Un resultado que contiene una colección de permisos asociados al rol o un Error si la operación falla.</returns>
        Response<List<Permission>> GetPermissionsByRoleID (GetPermissionsByRoleID_Query query);

        /// <summary>
        /// Actualiza un permiso existente de forma síncrona.
        /// </summary>
        /// <param name="command">Comando para actualizar un permiso.</param>
        /// <returns>Un resultado que contiene el permiso actualizado o un Error si la operación falla.</returns>
        Response<Permission> UpdatePermission (UpdatePermission_Command command);

        /// <summary>
        /// Elimina un permiso por su ID de forma síncrona.
        /// </summary>
        /// <param name="command">Comando para eliminar un permiso por su ID.</param>
        /// <returns>Un resultado que indica si el permiso fue eliminado exitosamente o un Error si la operación falla.</returns>
        Response<bool> DeletePermissionByID (DeletePermissionByID_Command command);

        #endregion

        #region Métodos asíncronos

        /// <summary>
        /// Agrega un permiso de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el permiso agregado o un Error si la operación falla.</returns>
        Task<Response<Permission>> AddPermissionAsync (AddPermission_Command command);

        /// <summary>
        /// Obtiene todos los permisos de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener todos los permisos.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de todos los permisos o un Error si la operación falla.</returns>
        Task<Response<List<Permission>>> GetPermissionsAsync (GetPermissions_Query query);

        /// <summary>
        /// Obtiene un permiso por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener un permiso por su ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el permiso obtenido o un Error si no se encuentra.</returns>
        Task<Response<Permission>> GetPermissionByIDAsync (GetPermissionByID_Query query);

        /// <summary>
        /// Obtiene los permisos asociados a un rol de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener los permisos asociados a un rol.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de permisos asociados al rol o un Error si la operación falla.</returns>
        Task<Response<List<Permission>>> GetPermissionsByRoleIDAsync (GetPermissionsByRoleID_Query query);

        /// <summary>
        /// Actualiza un permiso existente de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para actualizar un permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el permiso actualizado o un Error si la operación falla.</returns>
        Task<Response<Permission>> UpdatePermissionAsync (UpdatePermission_Command command);

        /// <summary>
        /// Elimina un permiso por su ID de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para eliminar un permiso por su ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si el permiso fue eliminado exitosamente o un Error si la operación falla.</returns>
        Task<Response<bool>> DeletePermissionByIDAsync (DeletePermissionByID_Command command);

        #endregion

    }

}