using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands {

    public interface IUpdateUser_Command : IUpdateEntity_Command<User> { }

}