namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Clase base para todos los errores de la aplicación.
    /// Incluye un código de error único y un mensaje de error.
    /// </summary>
    public class ApplicationError : Exception {

        /// <summary>
        /// Código de error único asociado al error.
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Constructor protegido de la clase ApplicationError.
        /// </summary>
        /// <param name="errorCode">Código de error único asociado al error.</param>
        /// <param name="message">Mensaje de error.</param>
        protected ApplicationError (string errorCode, string message, Exception? innerException = null) : base(message, innerException) =>
            ErrorCode = errorCode;

        /// <summary>
        /// Método estático para crear una instancia de ApplicationError.
        /// </summary>
        /// <param name="errorCode">Código de error único asociado al error.</param>
        /// <param name="message">Mensaje de error.</param>
        /// <returns>Una nueva instancia de ApplicationError.</returns>
        public static ApplicationError Create (string errorCode, string message, Exception? innerException = null) => new(errorCode, message, innerException);

        /// <summary>
        /// Método para lanzar una excepción basada en este error.
        /// </summary>
        /// <returns>Una excepción basada en este error.</returns>
        public ApplicationError Throw () => throw this;

    }

}