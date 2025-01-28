using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations {

    public class SystemLog_EntityTypeConfiguration : IEntityTypeConfiguration<SystemLog> {

        /// <summary>
        /// Configura la entidad «Entity» y sus propiedades en el modelo de la base de datos.
        /// </summary>
        /// <param name="systemLogModelBuilder">Builder para configurar la entidad «Entity».</param>
        public void Configure (EntityTypeBuilder<SystemLog> systemLogModelBuilder) {

            // Mapea la entidad a la tabla 'system_logs'.
            systemLogModelBuilder.ToTable("system_logs");

            // Configura la clave primaria de la entidad.
            systemLogModelBuilder.HasKey(systemLog => systemLog.ID);
            systemLogModelBuilder.Property(systemLog => systemLog.ID).HasColumnName("id");

            // Configura las propiedades principales.
            systemLogModelBuilder.Property(systemLog => systemLog.LogLevel)
                .HasColumnName("log_level")
                .HasColumnType("log_level")
                .IsRequired();

            systemLogModelBuilder.Property(systemLog => systemLog.Source)
                .HasColumnName("source")
                .HasMaxLength(80)
                .IsRequired();

            systemLogModelBuilder.Property(systemLog => systemLog.Message)
                .HasColumnName("message")
                .HasMaxLength(1000)
                .IsRequired();

            systemLogModelBuilder.Property(systemLog => systemLog.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            systemLogModelBuilder.Property(systemLog => systemLog.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            // Configura la columna «ID» como una clave foránea no relacionada.
            systemLogModelBuilder.Property(systemLog => systemLog.UserID)
                .HasColumnName("user_id")
                .IsRequired(false);

            // Configura el índice para la columna «user_id».
            systemLogModelBuilder
                .HasIndex(systemLog => systemLog.UserID)
                .HasDatabaseName("idx_system_logs_user_id");

            // Inicialización de datos (si aplica).
            InitializeData(systemLogModelBuilder);
        }

        /// <summary>
        /// Inicializa los datos de las entidades en la base de datos.
        /// </summary>
        /// <param name="systemLogModelBuilder">Builder para inicializar los datos.</param>
        private static void InitializeData (EntityTypeBuilder<SystemLog> systemLogModelBuilder) {
            // Aquí puedes agregar semillas de datos iniciales, si las necesitas.
        }

    }

}