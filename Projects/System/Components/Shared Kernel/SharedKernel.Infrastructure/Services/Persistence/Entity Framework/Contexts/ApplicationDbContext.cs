using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Contexts.EntityTypeConfigurations;

namespace SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Contexts {

    /// <summary>
    /// DbContext base para la aplicación, proporciona acceso a las entidades y configura las propiedades de las entidades.
    /// </summary>
    public abstract class ApplicationDbContext : DbContext {

        /// <summary>
        /// Conjunto de entidades `User` en la base de datos.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Conjunto de entidades `Role` en la base de datos.
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Conjunto de entidades `Permission` en la base de datos.
        /// </summary>
        public virtual DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// Conjunto de entidades `RolesAssignedToUser` en la base de datos.
        /// </summary>
        public virtual DbSet<RoleAssignedToUser> RolesAssignedToUsers { get; set; }

        /// <summary>
        /// Conjunto de entidades `PermissionAssignedToRole` en la base de datos.
        /// </summary>
        public virtual DbSet<PermissionAssignedToRole> PermissionsAssignedToRoles { get; set; }

        /// <summary>
        /// Conjunto de entidades `SystemLog` en la base de datos.
        /// </summary>
        public virtual DbSet<SystemLog> SystemLogs { get; set; }

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

            // Configura la entidad «User» y sus propiedades en el modelo de la base de datos.
            new User_EntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            // Configura la entidad «Role» y sus propiedades en el modelo de la base de datos.
            new Role_EntityTypeConfiguration().Configure(modelBuilder.Entity<Role>());
            // Configura la entidad «Permission» y sus propiedades en el modelo de la base de datos.
            new Permission_EntityTypeConfiguration().Configure(modelBuilder.Entity<Permission>());
            // Configura la entidad «RolesAssignedToUser» y sus propiedades en el modelo de la base de datos.
            new RoleAssignedToUser_EntityTypeConfiguration().Configure(modelBuilder.Entity<RoleAssignedToUser>());
            // Configura la entidad «PermissionAssignedToRole» y sus propiedades en el modelo de la base de datos.
            new PermissionsAssignedToRole_EntityTypeConfiguration().Configure(modelBuilder.Entity<PermissionAssignedToRole>());
            // Configura la entidad «SystemLog» y sus propiedades en el modelo de la base de datos.
            new SystemLog_EntityTypeConfiguration().Configure(modelBuilder.Entity<SystemLog>());

        }

    }

}