using API;
using ConsoleApplication.Layers;
using ConsoleApplication.Utils;
using ConsoleApplication.Utils.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Infrastructure.Services.Auth;
using System.Diagnostics;

namespace ConsoleApplication {

    internal enum SystemLayer {
        Services,
        Operators,
        API
    }

    internal class Program {

        private static bool _InMemoryContext { get; } = false;

        /// <summary>
        /// Punto de entrada principal de la aplicación de consola.
        /// </summary>
        /// <param name="args">Argumentos de la línea de comandos.</param>
        internal static void Main (string[] args) {

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


            // Crear el contexto de base de datos
            DbContext databaseInstance = _InMemoryContext ?
                DatabaseContext.Get_InMemory_DatabaseInstance() :
                DatabaseContext.Get_PostgresSQL_DatabaseInstance(configuration);

            // Stopwatch para medir el tiempo de ejecución
            Stopwatch stopwatch = new();

            try {

                stopwatch.Start();

                #region Inicialización de servicios de la aplicación
                Printer.PrintLine($"\n{"Inicializando los servicios de la aplicación".Underline()}");

                IAuthService authService = new AuthService(JWT.Get_Settings(configuration));
                Printer.PrintLine("- Servicio de autenticación inicializado exitosamente");

                IPersistenceService persistenceService = new PersistenceService(databaseInstance);
                Printer.PrintLine("- Servicio de persistencia inicializado exitosamente");

                // Instancia el administrador de la aplicación
                IApplicationManager applicationManager = new ApplicationManager(authService, persistenceService);
                Printer.PrintLine("- ApplicationManager inicializado exitosamente");
                #endregion

                var systemLayerToTest = SystemLayer.API;

                switch (systemLayerToTest) {

                    case SystemLayer.Services:
                        Main_Services_Layer.ExecuteTestFlow(authService, persistenceService);
                        break;

                    case SystemLayer.Operators:
                        Main_Operators_Layer.ExecuteTestFlow(applicationManager);
                        break;

                    case SystemLayer.API:
                        Main_API_Layer.ExecuteTestFlow(applicationManager);
                        break;

                    default:
                        Printer.PrintLine($"La capa especificada en {nameof(systemLayerToTest)} no es válida.");
                        break;

                }

            } catch (Exception ex) {

                Printer.PrintLine($"\n{"Error:".Underline()}");
                Printer.PrintLine(ex.Message);
                throw;

            } finally {

                databaseInstance.Dispose();
                stopwatch.Stop();
                Printer.PrintLine($"Tiempo de ejecución: {stopwatch.Elapsed.AsFormattedTime()}");

            }

        }

    }

}