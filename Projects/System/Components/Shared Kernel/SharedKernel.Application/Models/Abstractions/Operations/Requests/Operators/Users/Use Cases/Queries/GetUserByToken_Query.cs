using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Queries {

    /// <summary>
    /// Consulta para obtener un usuario mediante un token de autenticación.
    /// </summary>
    [RequiredPermissions([Enumerations.Permissions.GetUserByToken, Enumerations.Permissions.GetEntities])]
    public class GetUserByToken_Query : Operation {

        /// <summary>
        /// Obtiene o establece el token de autenticación.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByToken_Query"/>.
        /// </summary>
        /// <param name="token">El token de autenticación.</param>
        public GetUserByToken_Query (string token) => Token = token;

    }

}