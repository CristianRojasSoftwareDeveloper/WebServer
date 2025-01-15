using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.UseCases.Queries;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators {

    /// <summary>
    /// Interfaz para operaciones relacionadas con la gestión de los logs del sistema.
    /// </summary>
    public interface ISystemLogOperator : IReflexiveOperator {

        #region Métodos síncronos

        /// <summary>
        /// Agrega un log del sistema de forma síncrona.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo log del sistema.</param>
        /// <returns>Un resultado que contiene el log del sistema agregado o un Error si la operación falla.</returns>
        Response<SystemLog> AddSystemLog (AddSystemLog_Command command);

        /// <summary>
        /// Obtiene todos los logs del sistema de forma síncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener todos los logs del sistema.</param>
        /// <returns>Un resultado que contiene una colección de todos los logs del sistema o un Error si la operación falla.</returns>
        Response<List<SystemLog>> GetSystemLogs (GetSystemLogs_Query query);

        /// <summary>
        /// Obtiene un log del sistema por su ID de forma síncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener un log del sistema por su ID.</param>
        /// <returns>Un resultado que contiene el log del sistema obtenido o un Error si no se encuentra.</returns>
        Response<SystemLog> GetSystemLogByID (GetSystemLogByID_Query query);

        /// <summary>
        /// Obtiene los logs del sistema asociados a un usuario de forma síncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener los logs del sistema asociados a un usuario.</param>
        /// <returns>Un resultado que contiene una colección de logs del sistema asociados al usuario o un Error si la operación falla.</returns>
        Response<List<SystemLog>> GetSystemLogsByUserID (GetSystemLogsByUserID_Query query);

        /// <summary>
        /// Actualiza un log del sistema existente de forma síncrona.
        /// </summary>
        /// <param name="command">Comando para actualizar un log del sistema.</param>
        /// <returns>Un resultado que contiene el log del sistema actualizado o un Error si la operación falla.</returns>
        Response<SystemLog> UpdateSystemLog (UpdateSystemLog_Command command);

        /// <summary>
        /// Elimina un log del sistema por su ID de forma síncrona.
        /// </summary>
        /// <param name="command">Comando para eliminar un log del sistema por su ID.</param>
        /// <returns>Un resultado que indica si el log del sistema fue eliminado exitosamente o un Error si la operación falla.</returns>
        Response<bool> DeleteSystemLogByID (DeleteSystemLogByID_Command command);

        #endregion

        #region Métodos asíncronos

        /// <summary>
        /// Agrega un log del sistema de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo log del sistema.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el log del sistema agregado o un Error si la operación falla.</returns>
        Task<Response<SystemLog>> AddSystemLogAsync (AddSystemLog_Command command);

        /// <summary>
        /// Obtiene todos los logs del sistema de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener todos los logs del sistema.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de todos los logs del sistema o un Error si la operación falla.</returns>
        Task<Response<List<SystemLog>>> GetSystemLogsAsync (GetSystemLogs_Query query);

        /// <summary>
        /// Obtiene un log del sistema por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener un log del sistema por su ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el log del sistema obtenido o un Error si no se encuentra.</returns>
        Task<Response<SystemLog>> GetSystemLogByIDAsync (GetSystemLogByID_Query query);

        /// <summary>
        /// Obtiene los logs del sistema asociados a un usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">Consulta para obtener los logs del sistema asociados a un usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una colección de logs del sistema asociados al usuario o un Error si la operación falla.</returns>
        Task<Response<List<SystemLog>>> GetSystemLogsByUserIDAsync (GetSystemLogsByUserID_Query query);

        /// <summary>
        /// Actualiza un log del sistema existente de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para actualizar un log del sistema.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el log del sistema actualizado o un Error si la operación falla.</returns>
        Task<Response<SystemLog>> UpdateSystemLogAsync (UpdateSystemLog_Command command);

        /// <summary>
        /// Elimina un log del sistema por su ID de forma asíncrona.
        /// </summary>
        /// <param name="command">Comando para eliminar un log del sistema por su ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si el log del sistema fue eliminado exitosamente o un Error si la operación falla.</returns>
        Task<Response<bool>> DeleteSystemLogByIDAsync (DeleteSystemLogByID_Command command);

        #endregion

    }

}