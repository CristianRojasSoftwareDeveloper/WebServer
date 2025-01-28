using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands {

    public interface IAddEntity_Command<EntityType> : IOperation where EntityType : IGenericEntity {

        EntityType Entity { get; }

    }

}