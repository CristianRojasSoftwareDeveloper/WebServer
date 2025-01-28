using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.Use_Cases.Queries {

    public interface IGetRolesByUserID_Query : IOperation {

        int UserID { get; }

        bool EnableTracking { get; }

    }

}