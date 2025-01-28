using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Commands {

    public interface IAddRoleToUser_Command : IOperation {

        int RoleID { get; }

        int UserID { get; }

    }

}