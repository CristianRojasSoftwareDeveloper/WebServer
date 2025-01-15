using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.GenericRepositories;
using SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Repositories;
using System.Linq.Expressions;

namespace SystemLogs.Infrastructure.Services.Persistence.EntityFramework.Repositories {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework para gestionar logs del sistema.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class SystemLog_EntityFrameworkRepository : Generic_EntityFrameworkRepository<SystemLog>, ISystemLogRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio de logs del sistema.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public SystemLog_EntityFrameworkRepository (DbContext dbContext) : base(dbContext) { }

        #region Métodos síncronos

        /// <inheritdoc />
        public SystemLog AddSystemLog (SystemLog newSystemLog) =>
            AddEntity(newSystemLog, true, true);

        /// <inheritdoc />
        public List<SystemLog> GetSystemLogs () =>
            GetEntities();

        /// <inheritdoc />
        public SystemLog GetSystemLogByID (int systemLogID) =>
            GetEntityByID(systemLogID);

        /// <inheritdoc />
        public List<SystemLog> GetSystemLogsByUserID (int userID) =>
            GetQueryable().
            Where(systemLog => systemLog.UserID == userID).
            ToList();

        /// <inheritdoc />
        public SystemLog FirstSystemLogOrDefault (Expression<Func<SystemLog, bool>> predicate) =>
            FirstOrDefault(predicate);

        /// <inheritdoc />
        public SystemLog UpdateSystemLog (SystemLog systemLogUpdate) =>
            UpdateEntity(systemLogUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public bool DeleteSystemLogByID (int systemLogID) =>
            DeleteEntityByID(systemLogID);

        #endregion

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<SystemLog> AddSystemLogAsync (SystemLog newSystemLog) =>
            AddEntityAsync(newSystemLog, true, true);

        /// <inheritdoc />
        public Task<List<SystemLog>> GetSystemLogsAsync () =>
            GetEntitiesAsync();

        /// <inheritdoc />
        public Task<SystemLog> GetSystemLogByIDAsync (int systemLogID) =>
            GetEntityByIDAsync(systemLogID);

        /// <inheritdoc />
        public Task<List<SystemLog>> GetSystemLogsByUserIDAsync (int userID) =>
            GetQueryable().
            Where(systemLog => systemLog.UserID == userID).
            ToListAsync();

        /// <inheritdoc />
        public Task<SystemLog> FirstSystemLogOrDefaultAsync (Expression<Func<SystemLog, bool>> predicate) =>
            FirstOrDefaultAsync(predicate);

        /// <inheritdoc />
        public Task<SystemLog> UpdateSystemLogAsync (SystemLog systemLogUpdate) =>
            UpdateEntityAsync(systemLogUpdate, trySetUpdateDatetime: true);

        /// <inheritdoc />
        public Task<bool> DeleteSystemLogByIDAsync (int systemLogID) =>
            DeleteEntityByIDAsync(systemLogID);

        #endregion

    }

}