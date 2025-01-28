using API;
using ConsoleApplication.Layers;
using ConsoleApplication.Utils;
using ConsoleApplication.Utils.Configurations;
using Microsoft.Extensions.Configuration;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Infrastructure.Services.Auth;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using System.Diagnostics;
using SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Queries.GetSystemLogs;

namespace ConsoleApplication {

    internal enum SystemLayer {
        Services,
        Operators,
        API
    }

    internal class Program {

        private static bool _InMemoryContext { get; } = true;

        /// <summary>
        /// Punto de entrada principal de la aplicación de consola.
        /// </summary>
        /// <param name="args">Argumentos de la línea de comandos.</param>
        internal static async Task Main (string[] args) {

            // Crear una instancia de ConfigurationBuilder para construir la configuración de la aplicación
            var configuration = new ConfigurationBuilder()
                // Establece la ruta base para buscar archivos de configuración. En este caso, usamos la ruta donde se ejecuta la aplicación.
                .SetBasePath(AppContext.BaseDirectory)
                // Agrega el archivo JSON llamado "appsettings.json" como una fuente de configuración.
                // - `optional: false`: Indica que el archivo es obligatorio. Si no se encuentra, se lanzará una excepción.
                // - `reloadOnChange: true`: Permite que los cambios en el archivo se reflejen automáticamente en la configuración durante la ejecución de la aplicación.
                .AddJsonFile("app.settings.json", optional: false, reloadOnChange: true)
                // Agrega las variables de entorno del sistema operativo como fuente de configuración.
                // Esto es útil para sobrescribir configuraciones del archivo JSON o proporcionar valores sensibles
                // (como cadenas de conexión o claves API) de manera segura en entornos de despliegue.
                .AddEnvironmentVariables()
                // Construye el objeto de configuración (`IConfiguration`) con las fuentes previamente definidas.
                .Build();

            // Obtener la cadena de conexión
            string postgreSQL_ConnectionString = configuration.GetConnectionString("PostgreSQL") ??
                throw new InvalidOperationException("No se encontró la cadena de conexión para PostgreSQL en la configuración de la aplicación.");

            // Crear el contexto de base de datos
            ApplicationDbContext databaseInstance = _InMemoryContext ? new InMemory_DbContext() : new PostgreSQL_DbContext(postgreSQL_ConnectionString);

            // Stopwatch para medir el tiempo de ejecución
            Stopwatch stopwatch = new();

            try {

                stopwatch.Start();

                #region Inicialización de servicios de la aplicación
                Printer.PrintLine($"\n{"Inicializando los servicios de la aplicación".Underline()}");

                IAuthService authService = new AuthService(JWT.Get_Settings(configuration));
                Printer.PrintLine("- Servicio de autenticación inicializado exitosamente.");

                IPersistenceService persistenceService = new PersistenceService(databaseInstance);
                Printer.PrintLine("- Servicio de persistencia inicializado exitosamente.");

                // Instancia el administrador de la aplicación
                IApplicationManager applicationManager = new ApplicationManager(authService, persistenceService);
                Printer.PrintLine("- ApplicationManager inicializado exitosamente.");
                #endregion

                var systemLayerToTest = SystemLayer.API;

                switch (systemLayerToTest) {

                    case SystemLayer.Services:
                        await Main_Services_Layer.ExecuteTestFlow(authService, persistenceService);
                        break;

                    case SystemLayer.Operators:
                        await Main_Operators_Layer.ExecuteTestFlow(applicationManager);
                        break;

                    case SystemLayer.API:
                        await Main_API_Layer.ExecuteTestFlow(applicationManager);
                        break;

                    default:
                        Printer.PrintLine($"La capa especificada en {nameof(systemLayerToTest)} no es válida.");
                        break;

                }

                var resultingSystemLogs = await applicationManager.SystemLogOperator.GetSystemLogs(new GetSystemLogs_Query());
                foreach (var systemLog in resultingSystemLogs.Body!)
                    systemLog.PrintSystemLog();
            } catch (Exception ex) {

                Printer.PrintLine($"\n{"Error:".Underline()}");
                Printer.PrintLine(ex.Message);
                throw;

            } finally {

                databaseInstance.Dispose();
                stopwatch.Stop();
                Printer.PrintLine($"\nTiempo de ejecución: {stopwatch.Elapsed.AsFormattedTime()}");

            }

        }

    }

}