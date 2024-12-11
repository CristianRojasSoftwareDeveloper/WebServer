using WebServer; // Azure Function que proporciona la integración con Azure SignalR para un entorno serverless.
using Microsoft.Extensions.DependencyInjection; // Necesario para inyectar servicios en la aplicación.
using Microsoft.Extensions.Hosting; // Proporciona las clases base para configurar y ejecutar la aplicación.

// Configuración principal del host para la Azure Function
var host = new HostBuilder()
    // Configura los valores predeterminados para Azure Functions Worker en el proceso aislado.
    // El proceso aislado permite ejecutar Azure Functions con mayor flexibilidad y separación de ASP.NET Core.
    .ConfigureFunctionsWorkerDefaults(builder => {
        #region Registro de Azure SignalR en un contexto serverless.
        // «AddServerlessHub<ExtensionHub>()» permite definir el Hub (en este caso, `ExtensionHub`) que manejará las interacciones del servicio de SignalR.
        // Azure SignalR Service se encargará de gestionar las conexiones de los clientes y la comunicación con este Hub.
        builder.Services.AddServerlessHub<Server>();  // Registro de ExtensionHub para Azure SignalR.
        #endregion
    })
    // Se crea y construye el host con las configuraciones previamente definidas.
    .Build();

// Inicia la aplicación de Azure Functions. El host quedará a la espera de solicitudes para ejecutarlas.
host.Run();
