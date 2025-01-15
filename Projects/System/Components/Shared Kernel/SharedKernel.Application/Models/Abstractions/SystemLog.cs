using SharedKernel.Application.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Application.Models.Abstractions {

    /// <summary>
    /// Representa un log del sistema.
    /// </summary>
    public class SystemLog : GenericEntity {

        /// <summary>
        /// Nivel de severidad del log.
        /// </summary>
        public LogLevel? LogLevel { get; set; }

        /// <summary>
        /// Fuente del evento registrado.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Mensaje detallado del evento registrado.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Fecha y hora de creación del registro.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// ID del usuario asociado al evento registrado (opcional).
        /// Puede ser null si el evento no está asociado a ningún usuario específico.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// Usuario asociado al evento registrado (opcional).
        /// Esta propiedad es para uso interno de Entity Framework.
        /// No debe ser utilizada directamente en la lógica de negocio.
        /// </summary>
        public User? User { get; private set; }

        /// <summary>
        /// Constructor para crear un log del sistema.
        /// </summary>
        /// <param name="logLevel">Nivel de severidad del log.</param>
        /// <param name="source">Fuente del evento.</param>
        /// <param name="message">Mensaje detallado.</param>
        /// <param name="userID">ID del usuario asociado (opcional).</param>
        public SystemLog (LogLevel logLevel, string source, string message, int? userID = null) {
            LogLevel = logLevel;
            Source = source;
            Message = message;
            CreatedAt = DateTime.UtcNow;
            UserID = userID;
        }

    }

}