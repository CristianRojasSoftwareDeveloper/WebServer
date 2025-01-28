using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands {

    public interface IAddRole_Command : IAddEntity_Command<Role> { }

}