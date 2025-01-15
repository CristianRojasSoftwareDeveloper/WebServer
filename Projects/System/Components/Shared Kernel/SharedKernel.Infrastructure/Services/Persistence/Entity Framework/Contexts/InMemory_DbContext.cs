using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Contexts {

    /// <summary>
    /// DbContext para la base de datos en memoria.
    /// Proporciona acceso a las entidades y configura las propiedades de las entidades.
    /// </summary>
    public class InMemory_DbContext : ApplicationDbContext {

        /// <summary>
        /// Nombre de la base de datos en memoria.
        /// </summary>
        private string _databaseName { get; }

        /// <summary>
        /// Instancia singleton del contexto de base de datos en memoria.
        /// </summary>
        private static InMemory_DbContext? _instance { get; set; }

        /// <summary>
        /// Objeto para bloqueo de hilo seguro.
        /// </summary>
        private static object _lock { get; } = new();

        /// <summary>
        /// Constructor privado para evitar instanciación directa.
        /// </summary>
        /// <param name="databaseName">Nombre de la base de datos en memoria.</param>
        private InMemory_DbContext (string databaseName) =>
            _databaseName = databaseName;

        /// <summary>
        /// Obtiene la instancia singleton de InMemory_DbContext.
        /// </summary>
        /// <param name="databaseName">Nombre de la base de datos en memoria. Por defecto "InMemoryDb".</param>
        /// <returns>Instancia singleton de InMemory_DbContext.</returns>
        public static InMemory_DbContext Instance (string databaseName = "InMemoryDb") {
            // Comprueba si la instancia ya existe
            if (_instance == null) {
                // Bloqueo para evitar condiciones de carrera
                lock (_lock) {
                    // Inicializa la instancia si aún no existe
                    _instance ??= new InMemory_DbContext(databaseName);
                }
            }
            // Devuelve la instancia singleton
            return _instance;
        }

        /// <summary>
        /// Configura las opciones del contexto, como la base de datos en memoria.
        /// </summary>
        /// <param name="optionsBuilder">El generador de opciones de DbContext.</param>
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            // Comprueba si las opciones aún no están configuradas
            if (!optionsBuilder.IsConfigured) {
                // Llama a la configuración base
                base.OnConfiguring(optionsBuilder);
                // Configura la conexión a la base de datos en memoria
                optionsBuilder.UseInMemoryDatabase(_databaseName);
            }
        }

    }

}