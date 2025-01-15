using Microsoft.Extensions.Configuration;
using SharedKernel.Infrastructure.Services.Auth.Configurations;

namespace ConsoleApplication.Utils.Configurations {

    internal class JWT {

        internal static JWT_Settings Get_Settings (IConfigurationRoot configuration) {

            // Obtiene la sección de configuración de Json Web Token
            var jwtSettingsSection = configuration.GetSection("JWT_Settings") ??
                throw new InvalidOperationException("No se encontró la sección de configuración de Json Web Token en la configuración de la aplicación.");

            // Crear instancia de JWT_Settings utilizando los valores de la configuración
            var jwtSettings = new JWT_Settings(
                jwtSettingsSection.GetValue<string>("Key"),
                jwtSettingsSection.GetValue<string>("Issuer"),
                jwtSettingsSection.GetValue<string>("Audience"),
                jwtSettingsSection.GetValue<int>("ExpiryMinutes")
            );

            return jwtSettings;

        }

    }

}