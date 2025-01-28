using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.Use_Cases.Commands {
    public interface IRemovePermissionFromRole_Command : IOperation {
        int PermissionID { get; }
        int RoleID { get; }
    }
}