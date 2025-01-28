using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Queries {
    public interface IAuthenticateUser_Query : IOperation {
        string Password { get; set; }
        string Username { get; set; }
    }
}