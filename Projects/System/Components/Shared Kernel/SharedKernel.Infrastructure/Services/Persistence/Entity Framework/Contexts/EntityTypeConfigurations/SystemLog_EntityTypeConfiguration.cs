using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Application.Models.Abstractions;

namespace SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Contexts.EntityTypeConfigurations {

    public class SystemLog_EntityTypeConfiguration : IEntityTypeConfiguration<SystemLog> {

        /// <summary>
        /// Configura la entidad `SystemLog` y sus propiedades en el modelo de la base de datos.
        /// </summary>
        /// <param name="systemLogModelBuilder">Generador de modelo de registros de sistema.</param>
        public void Configure (EntityTypeBuilder<SystemLog> systemLogModelBuilder) {

            // Mapea la entidad a la tabla 'system_logs'
            systemLogModelBuilder.ToTable("system_logs");

            // Configura el identificador único de la entidad
            systemLogModelBuilder.Property(systemLog => systemLog.ID).HasColumnName("id");
            // Configura la clave primaria de la entidad
            systemLogModelBuilder.HasKey(systemLog => systemLog.ID);

            // Configura el nivel de severidad del log
            systemLogModelBuilder.Property(systemLog => systemLog.LogLevel)
                .HasColumnName("log_level")
                .HasColumnType("log_level")
                .IsRequired();

            // Configura la fuente del evento registrado
            systemLogModelBuilder.Property(systemLog => systemLog.Source)
                .HasColumnName("source")
                .HasMaxLength(80)
                .IsRequired();

            // Configura el mensaje del evento
            systemLogModelBuilder.Property(systemLog => systemLog.Message)
                .HasColumnName("message")
                .HasMaxLength(500)
                .IsRequired();

            // Configura la relación unidireccional con User
            systemLogModelBuilder
                .HasOne(systemLog => systemLog.User)
                .WithMany()  // Sin navegación inversa
                .HasForeignKey(systemLog => systemLog.UserID)
                .HasConstraintName("fk_system_logs_user")
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Configura el índice para la clave foránea user_id
            systemLogModelBuilder
                .HasIndex(systemLog => systemLog.UserID)
                .HasDatabaseName("idx_system_logs_user_id");

            // Ejecuta la inicialización de los datos de la base de datos.
            InitializeData(systemLogModelBuilder);

        }

        /// <summary>
        /// Inicializa los datos de las entidades en la base de datos.
        /// </summary>
        /// <param name="systemLogModelBuilder">Generador de modelo de registros de sistema.</param>
        private static void InitializeData (EntityTypeBuilder<SystemLog> systemLogModelBuilder) {
            // Semillas de datos para la entidad User
            systemLogModelBuilder.HasData([
                //new SystemLog {
                    // Aquí agregar los datos de los campos del registro de sistema inicial.
                //}
            ]);
        }

    }

}