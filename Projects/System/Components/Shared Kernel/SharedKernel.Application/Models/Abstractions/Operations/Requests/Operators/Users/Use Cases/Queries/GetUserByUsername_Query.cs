using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Queries {

    /// <summary>
    /// Consulta para obtener un usuario por su nombre de usuario.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetUserByUsername, Enumerations.Permissions.GetEntities])]
    public class GetUserByUsername_Query : Operation {

        /// <summary>
        /// Obtiene el nombre de usuario del usuario.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByUsername_Query"/>.
        /// </summary>
        /// <param name="username">El nombre de usuario del usuario a obtener.</param>
        public GetUserByUsername_Query (string username) => Username = username;

    }

}