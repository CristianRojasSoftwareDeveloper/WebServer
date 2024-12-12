using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations {

    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User> {

        /// <summary>
        /// Configura la entidad `User` y sus propiedades en el modelo de la base de datos.
        /// </summary>
        /// <param name="userModelBuilder">Generador de modelo de usuarios.</param>
        public void Configure (EntityTypeBuilder<User> userModelBuilder) {

            // Mapea la entidad a la tabla 'users'
            userModelBuilder.ToTable("users");

            // Configura el identificador único de la entidad
            userModelBuilder
                .Property(user => user.ID)
                .HasColumnName("id");

            // Configura la clave primaria de la entidad
            userModelBuilder
                .HasKey(user => user.ID);

            // Configura las propiedades de la entidad
            userModelBuilder.Property(user => user.Username)
                .HasColumnName("username")
                .HasMaxLength(20)
                .IsRequired(); // El nombre de usuario es obligatorio y tiene un límite de 20 caracteres

            userModelBuilder.Property(user => user.Password)
                .HasColumnName("password")
                .HasMaxLength(64)
                .IsRequired(); // La contraseña es obligatoria y tiene un límite de 64 caracteres

            userModelBuilder.Property(user => user.EncryptedPassword)
                .HasColumnName("encrypted_password")
                .HasMaxLength(64)
                .IsRequired(); // La contraseña encriptada es obligatoria y tiene un límite de 64 caracteres

            userModelBuilder.Property(user => user.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired(); // El nombre es obligatorio y tiene un límite de 50 caracteres

            userModelBuilder.Property(user => user.Email)
                .HasColumnName("email")
                .HasMaxLength(50)
                .IsRequired(); // El correo electrónico es obligatorio y tiene un límite de 50 caracteres

            userModelBuilder.Property(user => user.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired(); // La fecha y hora de creación se establece automáticamente y es obligatoria

            userModelBuilder.Property(user => user.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired(); // La fecha y hora de última actualización se establece automáticamente y es obligatoria

            // Configura los índices únicos
            userModelBuilder
                .HasIndex(user => user.Username)
                .IsUnique(); // Se asegura de que los nombres de usuario sean únicos

            InitializeData(userModelBuilder);

        }

        /// <summary>
        /// Inicializa los datos de las entidades en la base de datos.
        /// </summary>
        /// <param name="userModelBuilder">Generador de modelo de usuarios.</param>
        private static void InitializeData (EntityTypeBuilder<User> userModelBuilder) {
            // Semillas de datos para la entidad User
            userModelBuilder.HasData([
                new User {
                    ID = 1,
                    Username = "admin",
                    Email = "admin@gmail.com",
                    Name = "John Doe",
                    Password = "secret-key",
                    EncryptedPassword = "$2a$11$DEr.JIMcwp8lhjb4dyOu5Ob.aDZfLOVDHk9otvQjPv1Yi34GY3ZTK",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            ]);
        }

    }

}