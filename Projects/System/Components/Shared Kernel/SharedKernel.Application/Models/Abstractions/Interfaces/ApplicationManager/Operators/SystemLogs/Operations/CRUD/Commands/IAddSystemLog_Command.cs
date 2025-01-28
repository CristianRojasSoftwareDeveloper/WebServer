using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands {

    public interface IAddSystemLog_Command : IAddEntity_Command<SystemLog> { }

}