using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs {

    /// <summary>
    /// Interfaz para operaciones relacionadas con la gestión de los logs del sistema.
    /// </summary>
    public interface ISystemLogOperator : IReflexiveOperator {

        #region Métodos asíncronos

        /// <summary>
        /// Agrega un log del sistema de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo log del sistema.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el log del sistema agregado o un Error si la operación falla.</returns>
        Task<Response<SystemLog>> AddSystemLog (IAddSystemLog_Command command);

        /// <summary>
        /// Obtiene todos los logs del sistema de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener todos los logs del sistema.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de todos los logs del sistema o un Error si la operación falla.</returns>
        Task<Response<List<SystemLog>>> GetSystemLogs (IGetSystemLogs_Query query);

        /// <summary>
        /// Obtiene un log del sistema por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener un log del sistema por su ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el log del sistema obtenido o un Error si no se encuentra.</returns>
        Task<Response<SystemLog>> GetSystemLogByID (IGetSystemLogByID_Query query);

        /// <summary>
        /// Obtiene los logs del sistema asociados a un usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener los logs del sistema asociados a un usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de logs del sistema asociados al usuario o un Error si la operación falla.</returns>
        Task<Response<List<SystemLog>>> GetSystemLogsByUserID (IGetSystemLogsByUserID_Query query);

        /// <summary>
        /// Actualiza un log del sistema existente de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para actualizar un log del sistema.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el log del sistema actualizado o un Error si la operación falla.</returns>
        Task<Response<SystemLog>> UpdateSystemLog (IUpdateSystemLog_Command command);

        /// <summary>
        /// Elimina un log del sistema por su ID de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para eliminar un log del sistema por su ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si el log del sistema fue eliminado exitosamente o un Error si la operación falla.</returns>
        Task<Response<bool>> DeleteSystemLogByID (IDeleteSystemLogByID_Command command);

        #endregion

    }

}