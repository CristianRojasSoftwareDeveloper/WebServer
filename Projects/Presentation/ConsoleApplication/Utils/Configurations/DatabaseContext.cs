using Microsoft.Extensions.Configuration;
using SharedKernel.Infrastructure.Services.Persistence.EntityFramework.Contexts;

namespace ConsoleApplication.Utils.Configurations {

    internal class DatabaseContext {

        // Crea el contexto de base de datos en memoria.
        internal static InMemory_DbContext Get_InMemory_DatabaseInstance () => InMemory_DbContext.Instance();

        internal static PostgreSQL_DbContext Get_PostgresSQL_DatabaseInstance (IConfigurationRoot configuration) {

            // Obtener la cadena de conexión
            string postgreSQL_ConnectionString = configuration.GetConnectionString("PostgreSQL") ??
                throw new InvalidOperationException("No se encontró la cadena de conexión para PostgreSQL en la configuración de la aplicación.");

            // Crea el contexto de base de datos conectado a un servidor de base de datos PostgreSQL
            var PostgreSQL_DatabaseIntance = PostgreSQL_DbContext.Instance(postgreSQL_ConnectionString);

            // Retorna el contexto de base de datos
            return PostgreSQL_DatabaseIntance;

        }

    }

}