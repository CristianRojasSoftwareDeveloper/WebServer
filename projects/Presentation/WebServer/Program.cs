using WebServer; // Azure Function que proporciona la integraci�n con Azure SignalR para un entorno serverless.
using Microsoft.Extensions.DependencyInjection; // Necesario para inyectar servicios en la aplicaci�n.
using Microsoft.Extensions.Hosting; // Proporciona las clases base para configurar y ejecutar la aplicaci�n.

// Configuraci�n principal del host para la Azure Function
var host = new HostBuilder()
    // Configura los valores predeterminados para Azure Functions Worker en el proceso aislado.
    // El proceso aislado permite ejecutar Azure Functions con mayor flexibilidad y separaci�n de ASP.NET Core.
    .ConfigureFunctionsWorkerDefaults(builder => {
        #region Registro de Azure SignalR en un contexto serverless.
        // �AddServerlessHub<ExtensionHub>()� permite definir el Hub (en este caso, `ExtensionHub`) que manejar� las interacciones del servicio de SignalR.
        // Azure SignalR Service se encargar� de gestionar las conexiones de los clientes y la comunicaci�n con este Hub.
        builder.Services.AddServerlessHub<Server>();  // Registro de ExtensionHub para Azure SignalR.
        #endregion
    })
    // Se crea y construye el host con las configuraciones previamente definidas.
    .Build();

// Inicia la aplicaci�n de Azure Functions. El host quedar� a la espera de solicitudes para ejecutarlas.
host.Run();
