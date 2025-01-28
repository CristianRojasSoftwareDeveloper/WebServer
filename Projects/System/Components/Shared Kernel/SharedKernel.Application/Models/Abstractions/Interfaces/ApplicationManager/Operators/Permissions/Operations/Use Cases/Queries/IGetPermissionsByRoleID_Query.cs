using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.Use_Cases.Queries {

    public interface IGetPermissionsByRoleID_Query : IOperation {

        int RoleID { get; }

        bool EnableTracking { get; }

    }

}