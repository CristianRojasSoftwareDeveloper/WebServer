using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands {

    public interface IUpdatePermission_Command : IUpdateEntity_Command<Permission> { }

}