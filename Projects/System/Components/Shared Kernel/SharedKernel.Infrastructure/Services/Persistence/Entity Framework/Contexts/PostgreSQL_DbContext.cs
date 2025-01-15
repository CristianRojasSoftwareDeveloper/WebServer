using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Enumerations;

namespace SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Contexts {

    /// <summary>
    /// DbContext para la base de datos PostgreSQL.
    /// Proporciona acceso a las entidades y configura las propiedades de las entidades.
    /// </summary>
    public class PostgreSQL_DbContext : ApplicationDbContext {

        /// <summary>
        /// Cadena de conexión a la base de datos PostgreSQL.
        /// </summary>
        private string _connectionString { get; }

        /// <summary>
        /// Instancia singleton del contexto de base de datos PostgreSQL.
        /// </summary>
        private static PostgreSQL_DbContext? _instance { get; set; }

        /// <summary>
        /// Objeto para bloqueo de hilo seguro.
        /// </summary>
        private static object _lock { get; } = new();

        /// <summary>
        /// Constructor privado para evitar instanciación directa.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos PostgreSQL.</param>
        private PostgreSQL_DbContext (string connectionString) =>
            _connectionString = connectionString;

        /// <summary>
        /// Obtiene la instancia singleton de PostgreSQL_DbContext.
        /// </summary>
        /// <param name="connectionString">Cadena de conexión a la base de datos PostgreSQL.</param>
        /// <returns>Instancia singleton de PostgreSQL_DbContext.</returns>
        public static PostgreSQL_DbContext Instance (string connectionString) {
            // Comprueba si la instancia ya existe
            if (_instance == null) {
                // Bloqueo para evitar condiciones de carrera
                lock (_lock) {
                    // Inicializa la instancia si aún no existe
                    _instance ??= new PostgreSQL_DbContext(connectionString);
                }
            }
            // Devuelve la instancia singleton
            return _instance;
        }

        /// <summary>
        /// Configura las opciones del contexto, como la cadena de conexión a la base de datos.
        /// </summary>
        /// <param name="dbContextOptionsBuilder">El generador de opciones de DbContext.</param>
        protected override void OnConfiguring (DbContextOptionsBuilder dbContextOptionsBuilder) {
            // Comprueba si las opciones aún no están configuradas
            if (!dbContextOptionsBuilder.IsConfigured) {
                // Llama a la configuración base
                base.OnConfiguring(dbContextOptionsBuilder);
                // Configura la conexión a la base de datos PostgreSQL
                dbContextOptionsBuilder.UseNpgsql(_connectionString, dbContextOptionsBuilder => dbContextOptionsBuilder.MapEnum<LogLevel>("log_level"));
                // Habilita el comportamiento de marca de tiempo heredado para PostgreSQL
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
        }

    }

}