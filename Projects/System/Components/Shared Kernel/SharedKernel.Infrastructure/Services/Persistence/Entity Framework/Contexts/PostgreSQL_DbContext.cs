using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts {

    /// <summary>
    /// Implementación específica del DbContext para PostgreSQL.
    /// Configura y gestiona la conexión con una base de datos PostgreSQL, incluyendo el mapeo de enumeraciones
    /// y configuraciones específicas del proveedor.
    /// </summary>
    public class PostgreSQL_DbContext : ApplicationDbContext {

        /// <summary>
        /// Crea la configuración necesaria para la conexión a PostgreSQL.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión que especifica los parámetros de conexión a la base de datos PostgreSQL.</param>
        /// <returns>Opciones de configuración del contexto específicas para PostgreSQL.</returns>
        /// <remarks>
        /// Configura el mapeo del enum LogLevel y otras opciones específicas de PostgreSQL.
        /// </remarks>
        private static DbContextOptionsBuilder<PostgreSQL_DbContext> CreateConfiguration (string connectionString) =>
            new DbContextOptionsBuilder<PostgreSQL_DbContext>().UseNpgsql(connectionString, postgreSQL_dbContextOptionsBuilder =>
                postgreSQL_dbContextOptionsBuilder.MapEnum<LogLevel>("log_level"));

        /// <summary>
        /// Constructor estático que configura comportamientos globales específicos de PostgreSQL.
        /// </summary>
        /// <remarks>
        /// Habilita el comportamiento de marca de tiempo heredado para mantener la compatibilidad
        /// con versiones anteriores de PostgreSQL en el manejo de timestamps.
        /// </remarks>
        static PostgreSQL_DbContext () => AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        /// <summary>
        /// Inicializa una nueva instancia del contexto de base de datos PostgreSQL.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos PostgreSQL.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando connectionString está vacía.</exception>
        public PostgreSQL_DbContext (string connectionString) : base(CreateConfiguration(connectionString)) {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede estar vacía.");
        }

    }

}