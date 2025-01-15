namespace SharedKernel.Infrastructure.Services.Auth.Configurations {

    /// <summary>
    /// Clase que contiene la configuración relacionada con JWT.
    /// </summary>
    public class JWT_Settings {

        /// <summary>
        /// Obtiene o establece la clave secreta para firmar y validar el token.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Obtiene o establece el emisor del token.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Obtiene o establece la audiencia a la que va dirigido el token.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Obtiene o establece la duración de expiración del token en minutos.
        /// </summary>
        public int ExpiryMinutes { get; set; }

        /// <summary>
        /// Constructor de la clase JWT_Settings con valores validados.
        /// </summary>
        /// <param name="key">La clave secreta para firmar y validar el token.</param>
        /// <param name="issuer">El emisor del token.</param>
        /// <param name="audience">La audiencia a la que va dirigido el token.</param>
        /// <param name="expiryMinutes">La duración de expiración del token en minutos.</param>
        /// <exception cref="ArgumentNullException">Lanzada si algún argumento es nulo o vacío.</exception>
        /// <exception cref="ArgumentException">Lanzada si expiryMinutes es menor o igual a cero.</exception>
        public JWT_Settings (string key, string issuer, string audience, int expiryMinutes = 60) {
            Key = string.IsNullOrWhiteSpace(key) ? throw new ArgumentNullException(nameof(key), "La clave secreta no puede ser nula o vacía.") : key;
            Issuer = string.IsNullOrWhiteSpace(issuer) ? throw new ArgumentNullException(nameof(issuer), "El emisor no puede ser nulo o vacío.") : issuer;
            Audience = string.IsNullOrWhiteSpace(audience) ? throw new ArgumentNullException(nameof(audience), "La audiencia no puede ser nula o vacía.") : audience;
            ExpiryMinutes = expiryMinutes > 0 ? expiryMinutes : throw new ArgumentException("La duración de expiración debe ser mayor a cero.", nameof(expiryMinutes));
        }

    }

}