using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts {

    /// <summary>
    /// Implementación específica del DbContext para pruebas en memoria.
    /// Proporciona una base de datos en memoria para pruebas unitarias y desarrollo,
    /// sin necesidad de una base de datos real.
    /// </summary>
    /// <remarks>
    /// Esta implementación es útil para pruebas unitarias y escenarios de desarrollo
    /// donde no se requiere persistencia real de datos.
    /// </remarks>
    public class InMemory_DbContext : ApplicationDbContext {
        /// <summary>
        /// Crea la configuración necesaria para la base de datos en memoria.
        /// </summary>
        /// <param name="databaseName">Nombre que identifica la instancia de la base de datos en memoria.</param>
        /// <returns>Opciones de configuración del contexto específicas para la base de datos en memoria.</returns>
        private static DbContextOptionsBuilder<InMemory_DbContext> CreateConfiguration (string databaseName) =>
            new DbContextOptionsBuilder<InMemory_DbContext>().UseInMemoryDatabase(databaseName);

        /// <summary>
        /// Inicializa una nueva instancia del contexto de base de datos en memoria.
        /// </summary>
        /// <param name="databaseName">
        /// Nombre que identifica la instancia de la base de datos en memoria.
        /// Si no se especifica, se usa "InMemoryDb" por defecto.
        /// </param>
        /// <exception cref="ArgumentNullException">Se lanza cuando databaseName está vacío.</exception>
        /// <remarks>
        /// Cada nombre de base de datos crea una instancia separada en memoria.
        /// Usar el mismo nombre en diferentes instancias permite compartir los datos entre ellas.
        /// </remarks>
        public InMemory_DbContext (string databaseName = "InMemoryDb") : base(CreateConfiguration(databaseName)) {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentNullException(nameof(databaseName), "El nombre de la base de datos en memoria no puede estar vacío.");
        }

    }

}