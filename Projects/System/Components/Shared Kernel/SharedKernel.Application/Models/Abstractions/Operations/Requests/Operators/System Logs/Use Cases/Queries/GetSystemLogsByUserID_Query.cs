using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.SystemLogs.UseCases.Queries {

    /// <summary>
    /// Consulta para obtener los logs del sistema asociados a un usuario según su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetSystemLogsByUserID, Enumerations.Permissions.GetEntities])]
    public class GetSystemLogsByUserID_Query : Operation {

        /// <summary>
        /// ID del usuario del cual se intentan obtener logs asociados.
        /// </summary>
        public int UserID { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del usuario especificado.
        /// </summary>
        /// <param name="userID">ID del usuario del cual se buscarán logs asociados.</param>
        public GetSystemLogsByUserID_Query (int userID) => UserID = userID;

    }

}