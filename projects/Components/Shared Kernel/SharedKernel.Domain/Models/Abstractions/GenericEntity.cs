using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Domain.Models.Abstractions {

    /// <summary>
    /// Representa una implementación abstracta de una entidad genérica con un identificador único.
    /// </summary>
    public abstract class GenericEntity : IGenericEntity {

        /// <inheritdoc />
        public int? ID { get; set; }

        protected GenericEntity (int? ID = null) => this.ID = ID;

    }

}
