using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace SystemLogs.Infrastructure.Services.Persistence.Entity_Framework.Repositories {

    /// <summary>
    /// Implementación abstracta de un repositorio genérico utilizando Entity Framework para gestionar logs del sistema.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public class SystemLog_EntityFrameworkRepository : Generic_EntityFrameworkRepository<SystemLog>, ISystemLogRepository {

        /// <summary>
        /// Inicializa una nueva instancia del repositorio de logs del sistema.
        /// </summary>
        /// <param name="dbContext">El contexto de base de datos de Entity Framework.</param>
        public SystemLog_EntityFrameworkRepository (ApplicationDbContext dbContext) : base(dbContext) { }

        #region Métodos asíncronos

        /// <inheritdoc />
        public Task<SystemLog> AddSystemLog (SystemLog newSystemLog) =>
            AddEntity(newSystemLog);

        /// <inheritdoc />
        public Task<List<SystemLog>> GetSystemLogs (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <inheritdoc />
        public Task<SystemLog?> GetSystemLogByID (int systemLogID, bool enableTracking = false) =>
            GetEntityByID(systemLogID, enableTracking);

        /// <inheritdoc />
        public Task<List<SystemLog>> GetSystemLogsByUserID (int userID, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(systemLog => systemLog.UserID == userID).
            ToListAsync();

        /// <inheritdoc />
        public Task<SystemLog> UpdateSystemLog (Partial<SystemLog> systemLogUpdate) =>
            UpdateEntity(systemLogUpdate);

        /// <inheritdoc />
        public Task<bool> DeleteSystemLogByID (int systemLogID) =>
            DeleteEntityByID(systemLogID);

        #endregion

    }

}