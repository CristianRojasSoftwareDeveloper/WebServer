namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers {

    /// <summary>
    /// Interfaz para manejar operaciones de forma síncrona.
    /// </summary>
    /// <typeparam name="OperationInputType">Tipo de entrada de la operación.</typeparam>
    /// <typeparam name="ResponseType">Tipo de resultado de la operación.</typeparam>
    public interface ISyncOperationHandler<OperationInputType, ResponseType> {
        /// <summary>
        /// Ejecuta la operación de forma síncrona.
        /// </summary>
        /// <param name="operationInput">Entrada de la operación.</param>
        /// <returns>El resultado de la operación.</returns>
        ResponseType Handle (OperationInputType operationInput);

    }

}