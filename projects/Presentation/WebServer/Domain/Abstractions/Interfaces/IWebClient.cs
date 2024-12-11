namespace WebServer.Domain.Abstractions.Interfaces {

    public interface IWebClient {

        Task SendMessage (string message);

    }

}