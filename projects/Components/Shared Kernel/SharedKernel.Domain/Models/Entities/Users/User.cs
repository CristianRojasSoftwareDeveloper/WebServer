using SharedKernel.Domain.Models.Abstractions;

namespace SharedKernel.Domain.Models.Entities.Users {

    /// <summary>
    /// Entidad que representa un usuario en el sistema.
    /// Contiene información básica del usuario como nombre, nombre de usuario, correo electrónico, etc.
    /// </summary>
    public class User : GenericEntity {

        /// <summary>
        /// Nombre de usuario, utilizado para el inicio de sesión.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Contraseña del usuario en texto claro.
        /// No debe ser almacenada directamente en la base de datos.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Contraseña encriptada del usuario.
        /// Debe ser utilizada para la autenticación.
        /// </summary>
        public string? EncryptedPassword { get; set; }

        /// <summary>
        /// Fecha y hora en que el usuario fue creado en el sistema.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora en que el usuario fue actualizado por última vez en el sistema.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Imprime la información del usuario en la consola.
        /// </summary>
        public void PrintUser () {

            // Función local para formatear valores de cadena nulos.
            string FormatValue (string? value) => value ?? "No especificado";
            // Función local para formatear valores de fecha nulos.
            string FormatDate (DateTime? date) => date?.ToString("yyyy-MM-dd HH:mm:ss") ?? "No especificada";

            try {
                // Imprime información básica del usuario.
                Console.WriteLine("   - Información del Usuario:");
                Console.WriteLine($"\tID: {ID}");
                Console.WriteLine($"\tUsername: {FormatValue(Username)}");
                Console.WriteLine($"\tEmail: {FormatValue(Email)}");
                Console.WriteLine($"\tName: {FormatValue(Name)}");
                Console.WriteLine($"\tPassword: {FormatValue(Password)}");
                Console.WriteLine($"\tEncrypted Password: {FormatValue(EncryptedPassword)}");
                Console.WriteLine($"\tCreated At: {FormatDate(CreatedAt)}");
                Console.WriteLine($"\tUpdated At: {FormatDate(UpdatedAt)}");
            } catch (Exception ex) {
                // Manejo de excepciones al imprimir la información del usuario.
                Console.WriteLine($"Ha ocurrido un error al imprimir la información del usuario: {ex.Message}");
            }

        }

        /// <summary>
        /// Determina si el objeto especificado es igual al objeto actual.
        /// </summary>
        /// <param name="obj">El objeto a comparar con el objeto actual.</param>
        /// <returns>true si el objeto especificado es igual al objeto actual; de lo contrario, false.</returns>
        public override bool Equals (object? obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otherUser = (User) obj;
            return ID == otherUser.ID &&
                   Username == otherUser.Username &&
                   Email == otherUser.Email &&
                   Name == otherUser.Name &&
                   Password == otherUser.Password &&
                   EncryptedPassword == otherUser.EncryptedPassword;
        }

        /// <summary>
        /// Sirve como la función hash predeterminada.
        /// </summary>
        /// <returns>Un código hash para el objeto actual.</returns>
        public override int GetHashCode () =>
            // Un buen enfoque para GetHashCode es utilizar los mismos campos que en Equals.
            HashCode.Combine(ID, Username, Email, Name, Password, EncryptedPassword);

    }

}