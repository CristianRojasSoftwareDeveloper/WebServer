using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Domain.Models.Abstractions {

    /// <summary>
    /// Representa una implementación abstracta de una entidad genérica con un identificador único.
    /// </summary>
    public abstract class GenericEntity : IGenericEntity {

        /// <inheritdoc />
        public int? ID { get; set; }

        protected GenericEntity (int? identifier = null) => ID = identifier;

        // Método heredable para formatear valores de cadena nulos, devolviendo un texto predeterminado si el valor es null.
        protected string FormatStringValue (string? value) => value ?? "No especificado";

        // Método heredable para formatear valores de fecha nulos, devolviendo un texto predeterminado si el valor es null.
        protected string FormatDateTime (DateTime? date) => date?.ToString("yyyy-MM-dd HH:mm:ss") ?? "No especificada";




    }

}