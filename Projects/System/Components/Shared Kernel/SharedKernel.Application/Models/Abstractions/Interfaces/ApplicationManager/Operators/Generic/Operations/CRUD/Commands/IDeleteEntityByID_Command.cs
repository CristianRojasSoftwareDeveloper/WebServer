namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands {

    public interface IDeleteEntityByID_Command : IOperation {

        int ID { get; }

    }

}