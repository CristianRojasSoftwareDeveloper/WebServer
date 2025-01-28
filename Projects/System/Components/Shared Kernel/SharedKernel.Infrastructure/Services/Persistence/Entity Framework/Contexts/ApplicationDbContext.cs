using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Models.Abstractions.Interfaces;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts {

    /// <summary>
    /// Clase base abstracta que define el contexto de base de datos común para la aplicación.
    /// Proporciona acceso a las entidades y maneja la configuración del modelo de datos,
    /// incluyendo el seguimiento automático de fechas de creación y actualización.
    /// </summary>
    /// <remarks>
    /// Esta clase implementa el patrón Unit of Work y Repository a través de Entity Framework Core.
    /// Las clases derivadas deben proporcionar la configuración específica del proveedor de base de datos.
    /// </remarks>
    public abstract class ApplicationDbContext : DbContext {

        private static bool ActiveLog { get; } = true;

        private static string LogFileName { get; } = "system-database.log";

        // Propiedad estática de solo lectura para el nombre del archivo de log
        private static string LogFilePath { get; } = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, LogFileName);

        private static DbContextOptions CreateConfiguration (DbContextOptionsBuilder configuration) =>
            (!ActiveLog ? configuration : configuration.LogTo(WriteLogToFile)).Options;// Constructor estático para vaciar el archivo de log al iniciar la aplicación

        static ApplicationDbContext () {
            if (ActiveLog && File.Exists(LogFilePath))
                File.Delete(LogFilePath);  // Vacia el archivo de log si ya existe
        }

        /// <summary>
        /// Método para escribir el log en un archivo de texto.
        /// </summary>
        private static void WriteLogToFile (string log) {
            try {
                // Abre el archivo para escribir (crea el archivo si no existe)
                using StreamWriter writer = new(LogFilePath, append: true);
                writer.WriteLine($"[{DateTime.Now}]: {log}");
            } catch (Exception ex) {
                // En caso de error, imprime la ruta y el mensaje de la excepción
                Console.WriteLine($"Ha ocurrido un error al intentar escribir en el archivo de log [{LogFilePath}]: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene o establece el conjunto de usuarios en la base de datos.
        /// Permite realizar operaciones CRUD sobre la entidad Entity.
        /// </summary>
        /// <remarks>
        /// La propiedad es virtual para permitir el mocking en pruebas unitarias.
        /// </remarks>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Obtiene o establece el conjunto de roles en la base de datos.
        /// Permite realizar operaciones CRUD sobre la entidad Entity.
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Obtiene o establece el conjunto de permisos en la base de datos.
        /// Permite realizar operaciones CRUD sobre la entidad Entity.
        /// </summary>
        public virtual DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// Obtiene o establece el conjunto de asignaciones de roles a usuarios.
        /// Permite gestionar la relación muchos a muchos entre usuarios y roles.
        /// </summary>
        public virtual DbSet<RoleAssignedToUser> RolesAssignedToUsers { get; set; }

        /// <summary>
        /// Obtiene o establece el conjunto de asignaciones de permisos a roles.
        /// Permite gestionar la relación muchos a muchos entre roles y permisos.
        /// </summary>
        public virtual DbSet<PermissionAssignedToRole> PermissionsAssignedToRoles { get; set; }

        /// <summary>
        /// Obtiene o establece el conjunto de logs del sistema.
        /// Permite el registro y consulta de eventos del sistema.
        /// </summary>
        public virtual DbSet<SystemLog> SystemLogs { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia del contexto con las opciones especificadas.
        /// </summary>
        /// <param name="options">Opciones de configuración para el contexto de base de datos.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando options es null.</exception>
        public ApplicationDbContext (DbContextOptionsBuilder configuration) : base(CreateConfiguration(configuration)) { }

        /// <summary>
        /// Configura el modelo de la base de datos usando Fluent API.
        /// </summary>
        /// <param name="modelBuilder">Builder utilizado para construir el modelo.</param>
        /// <remarks>
        /// Este método aplica las configuraciones específicas de cada entidad
        /// definidas en sus respectivas clases de configuración.
        /// </remarks>
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            ArgumentNullException.ThrowIfNull(modelBuilder);
            new User_EntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new Role_EntityTypeConfiguration().Configure(modelBuilder.Entity<Role>());
            new Permission_EntityTypeConfiguration().Configure(modelBuilder.Entity<Permission>());
            new RoleAssignedToUser_EntityTypeConfiguration().Configure(modelBuilder.Entity<RoleAssignedToUser>());
            new PermissionsAssignedToRole_EntityTypeConfiguration().Configure(modelBuilder.Entity<PermissionAssignedToRole>());
            new SystemLog_EntityTypeConfiguration().Configure(modelBuilder.Entity<SystemLog>());
        }

        /// <summary>
        /// Actualiza las propiedades de seguimiento (CreatedAt y UpdatedAt) de las entidades
        /// que implementan ITrackeable antes de guardar los cambios.
        /// </summary>
        /// <remarks>
        /// Establece la fecha de creación para entidades nuevas y
        /// actualiza la fecha de modificación para entidades modificadas.
        /// </remarks>
        private void UpdateTrackingProperties () {
            var utcNow = DateTime.UtcNow;
            var entries = ChangeTracker.Entries<ITrackeable>().Where(e => e.State is EntityState.Added or EntityState.Modified);
            foreach (var entry in entries) {
                entry.Entity.UpdatedAt = utcNow;
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = utcNow;
            }
        }

        /// <summary>
        /// Guarda todos los cambios realizados en el contexto de forma asíncrona.
        /// </summary>
        /// <param name="cancellationToken">Token para cancelar la operación asíncrona.</param>
        /// <returns>
        /// Número de entidades modificadas en la base de datos.
        /// </returns>
        /// <remarks>
        /// Actualiza automáticamente las propiedades de seguimiento antes de guardar los cambios.
        /// </remarks>
        public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) {
            UpdateTrackingProperties();
            int entriesWritten;
            try {
                entriesWritten = await base.SaveChangesAsync(cancellationToken);
            } catch (Exception ex) {
                throw;
            }
            return entriesWritten;
        }

    }

}