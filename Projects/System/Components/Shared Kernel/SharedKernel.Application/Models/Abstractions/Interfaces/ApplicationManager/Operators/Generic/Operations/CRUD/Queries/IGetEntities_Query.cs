namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries {

    public interface IGetEntities_Query : IOperation {

        bool EnableTracking { get; }

    }

}