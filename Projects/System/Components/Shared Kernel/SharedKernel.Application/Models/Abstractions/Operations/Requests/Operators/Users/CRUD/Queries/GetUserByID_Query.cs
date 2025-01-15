using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Queries {

    /// <summary>
    /// Consulta para obtener un usuario por su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetUserByID, Enumerations.Permissions.GetEntityByID])]
    public class GetUserByID_Query : Operation {

        /// <summary>
        /// Obtiene o establece el ID del usuario.
        /// </summary>
        public int UserID { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener un usuario por su ID.
        /// </summary>
        /// <param name="userID">El ID del usuario a obtener.</param>
        public GetUserByID_Query (int userID) => UserID = userID;

    }

}