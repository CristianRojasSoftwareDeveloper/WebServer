using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Roles.UseCases.Queries {

    /// <summary>
    /// Consulta para obtener los roles de un usuario por su ID.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetRolesByUserID, Enumerations.Permissions.GetEntities])]
    public class GetRolesByUserID_Query : Operation {

        /// <summary>
        /// Obtiene el ID del usuario para el que se obtendrán los roles.
        /// </summary>
        public int UserID { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener los roles de un usuario por su ID.
        /// </summary>
        /// <param name="userID">El ID del usuario para el que se obtendrán los roles.</param>
        public GetRolesByUserID_Query (int userID) {
            UserID = userID;
        }

    }

}