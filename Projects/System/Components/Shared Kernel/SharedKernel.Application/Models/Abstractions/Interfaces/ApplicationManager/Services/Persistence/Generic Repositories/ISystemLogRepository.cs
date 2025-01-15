namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories {

    public interface ISystemLogRepository : IQueryableRepository<SystemLog> {

        #region Métodos síncronos específicos de logs del sistema

        /// <summary>
        /// Agrega un nuevo log del sistema al repositorio.
        /// </summary>
        /// <param name="newSystemLog">El log del sistema a agregar.</param>
        /// <returns>El log del sistema agregado.</returns>
        SystemLog AddSystemLog (SystemLog newSystemLog);

        /// <summary>
        /// Obtiene todos los logs del sistema del repositorio.
        /// </summary>
        /// <returns>Una colección de logs del sistema.</returns>
        List<SystemLog> GetSystemLogs ();

        /// <summary>
        /// Obtiene un log del sistema por su ID.
        /// </summary>
        /// <param name="systemLogID">El ID del log del sistema.</param>
        /// <returns>El log del sistema encontrado o null si no existe.</returns>
        SystemLog GetSystemLogByID (int systemLogID);

        /// <summary>
        /// Obtiene todos los logs del sistema asociados a un determinado usuario según su ID.
        /// </summary>
        /// <param name="userID">ID del usuario del cual se buscarán logs asociados.</param>
        /// <returns>Una colección de logs del sistema.</returns>
        List<SystemLog> GetSystemLogsByUserID (int userID);

        /// <summary>
        /// Actualiza un log del sistema existente por su ID.
        /// </summary>
        /// <param name="updatedSystemLog">El log del sistema actualizado.</param>
        /// <returns>El log del sistema actualizado o null si no se encontró.</returns>
        SystemLog UpdateSystemLog (SystemLog updatedSystemLog);

        /// <summary>
        /// Elimina un log del sistema por su ID.
        /// </summary>
        /// <param name="systemLogID">El ID del log del sistema a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, false si no se encontró el log del sistema.</returns>
        bool DeleteSystemLogByID (int systemLogID);

        #endregion

        #region Métodos asíncronos específicos de logs del sistema

        /// <summary>
        /// Agrega un nuevo log del sistema al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newSystemLog">El log del sistema a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el log del sistema agregado como resultado.</returns>
        Task<SystemLog> AddSystemLogAsync (SystemLog newSystemLog);

        /// <summary>
        /// Obtiene todos los logs del sistema del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de logs del sistema como resultado.</returns>
        Task<List<SystemLog>> GetSystemLogsAsync ();

        /// <summary>
        /// Obtiene un log del sistema por su ID de forma asíncrona.
        /// </summary>
        /// <param name="systemLogID">El ID del log del sistema.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el log del sistema encontrado o null si no existe como resultado.</returns>
        Task<SystemLog> GetSystemLogByIDAsync (int systemLogID);

        /// <summary>
        /// Obtiene todos los logs del sistema asociados a un determinado usuario según su ID.
        /// </summary>
        /// <param name="userID">ID del usuario del cual se buscarán logs asociados.</param>
        /// <returns>Una colección de logs del sistema.</returns>
        Task<List<SystemLog>> GetSystemLogsByUserIDAsync (int userID);

        /// <summary>
        /// Actualiza un log del sistema existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="updatedSystemLog">El log del sistema actualizado.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el log del sistema actualizado o null si no se encontró como resultado.</returns>
        Task<SystemLog> UpdateSystemLogAsync (SystemLog updatedSystemLog);

        /// <summary>
        /// Elimina un log del sistema por su ID de forma asíncrona.
        /// </summary>
        /// <param name="systemLogID">El ID del log del sistema a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<bool> DeleteSystemLogByIDAsync (int systemLogID);

        #endregion

    }

}