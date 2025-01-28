using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands {

    public interface IUpdateRole_Command : IUpdateEntity_Command<Role> { }

}