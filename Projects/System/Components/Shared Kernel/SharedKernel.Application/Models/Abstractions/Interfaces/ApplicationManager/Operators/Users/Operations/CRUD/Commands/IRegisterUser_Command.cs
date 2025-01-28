using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands {

    public interface IRegisterUser_Command : IAddEntity_Command<User> {

        int[] AssociatedRolesIdentifiers { get; }

    }

}