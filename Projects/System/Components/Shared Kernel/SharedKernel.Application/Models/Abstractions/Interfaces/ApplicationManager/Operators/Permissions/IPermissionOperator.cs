using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions {

    /// <summary>
    /// Interfaz para operaciones relacionadas con la gestión de los permisos de usuario.
    /// </summary>
    public interface IPermissionOperator : IReflexiveOperator {

        #region Métodos asíncronos

        /// <summary>
        /// Agrega un permiso de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el permiso agregado o un Error si la operación falla.</returns>
        Task<Response<Permission>> AddPermission (IAddPermission_Command command);

        /// <summary>
        /// Obtiene todos los permisos de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener todos los permisos.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de todos los permisos o un Error si la operación falla.</returns>
        Task<Response<List<Permission>>> GetPermissions (IGetPermissions_Query query);

        /// <summary>
        /// Obtiene un permiso por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener un permiso por su ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el permiso obtenido o un Error si no se encuentra.</returns>
        Task<Response<Permission>> GetPermissionByID (IGetPermissionByID_Query query);

        /// <summary>
        /// Obtiene los permisos asociados a un rol de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener los permisos asociados a un rol.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de permisos asociados al rol o un Error si la operación falla.</returns>
        Task<Response<List<Permission>>> GetPermissionsByRoleID (IGetPermissionsByRoleID_Query query);

        /// <summary>
        /// Actualiza un permiso existente de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para actualizar un permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el permiso actualizado o un Error si la operación falla.</returns>
        Task<Response<Permission>> UpdatePermission (IUpdatePermission_Command command);

        /// <summary>
        /// Elimina un permiso por su ID de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para eliminar un permiso por su ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si el permiso fue eliminado exitosamente o un Error si la operación falla.</returns>
        Task<Response<bool>> DeletePermissionByID (IDeletePermissionByID_Command command);

        #endregion

    }

}