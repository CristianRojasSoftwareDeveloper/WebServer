namespace WebServer.Abstractions.Interfaces {

    public interface IWebClient {

        Task SendMessage (string message);

    }

}