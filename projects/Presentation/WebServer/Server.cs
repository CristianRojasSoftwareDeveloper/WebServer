using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.SignalRService;
using Microsoft.Extensions.Logging;
using System.Net;
using WebServer.Abstractions.Interfaces;

namespace WebServer {

    [SignalRConnection("AzureSignalRConnectionString")]
    public class Server : ServerlessHub<IWebClient> {

        private const string HubName = nameof(Server);
        private readonly ILogger<Server> _logger;

        public Server (IServiceProvider serviceProvider, ILogger<Server> logger) : base(serviceProvider) => _logger = logger;

        [Function("Login")]
        public HttpResponseData Login ([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData request) {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = request.CreateResponse(HttpStatusCode.OK);
            response.WriteString("Welcome to Azure Functions!");
            return response;
        }

        [Function("negotiate")]
        public async Task<HttpResponseData> Negotiate ([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req) {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var negotiateResponse = await NegotiateAsync(new() { UserId = req.Headers.GetValues("UserID").FirstOrDefault() });
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteBytes(negotiateResponse.ToArray());
            return response;
        }

        [Function("HelloServer")]
        public HttpResponseData HelloServer ([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req) {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString("Hello, World!");
            return response;
        }

        [Function("HelloClient")]
        public Task HelloClient ([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req) {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return Clients.All.SendMessage("Hello client!");
        }

    }

}