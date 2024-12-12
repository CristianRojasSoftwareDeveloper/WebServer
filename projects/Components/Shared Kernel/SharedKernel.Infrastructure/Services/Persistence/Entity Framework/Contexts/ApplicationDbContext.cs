using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts {

    /// <summary>
    /// DbContext base para la aplicación, proporciona acceso a las entidades y configura las propiedades de las entidades.
    /// </summary>
    public abstract class ApplicationDbContext : DbContext {

        /// <summary>
        /// Conjunto de entidades `User` en la base de datos.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Configura las opciones del contexto, como la cadena de conexión a la base de datos.
        /// </summary>
        /// <param name="optionsBuilder">El generador de opciones de DbContext.</param>
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            // Comprueba si las opciones aún no están configuradas
            if (!optionsBuilder.IsConfigured) {
                // Llama a la configuración base
                base.OnConfiguring(optionsBuilder);
                // Configura el comportamiento de seguimiento de consultas como NoTracking por defecto
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        /// <summary>
        /// Configura las entidades y sus propiedades en el modelo de la base de datos.
        /// </summary>
        /// <param name="modelBuilder">El generador de modelos.</param>
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            // Configura la entidad `User` y sus propiedades en el modelo de la base de datos.
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
        }

    }

}