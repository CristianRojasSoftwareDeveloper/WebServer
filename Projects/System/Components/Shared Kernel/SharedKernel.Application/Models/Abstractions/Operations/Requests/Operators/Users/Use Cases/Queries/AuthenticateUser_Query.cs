using SharedKernel.Application.Models.Abstractions.Attributes;

namespace SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Queries {

    /// <summary>
    /// Consulta para autenticar a un usuario.
    /// </summary>
    [RequiredPermissions(Enumerations.Permissions.AuthenticateUser)]
    public class AuthenticateUser_Query : Operation {

        /// <summary>
        /// Obtiene o establece el nombre de usuario.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para autenticar a un usuario.
        /// </summary>
        /// <param name="username">El nombre de usuario.</param>
        /// <param name="password">La contraseña del usuario.</param>
        public AuthenticateUser_Query (string username, string password) {
            Username = username;
            Password = password;
        }

    }

}