namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Handlers {

    /// <summary>
    /// Interfaz para manejar operaciones de forma asíncrona.
    /// </summary>
    /// <typeparam name="OperationInputType">Tipo de entrada de la operación.</typeparam>
    /// <typeparam name="ResponseType">Tipo de resultado de la operación.</typeparam>
    public interface IAsyncOperationHandler<OperationInputType, ResponseType> {

        /// <summary>
        /// Ejecuta la operación de forma asíncrona.
        /// </summary>
        /// <param name="operationInput">Entrada de la operación.</param>
        /// <returns>Una tarea que representa el resultado de la operación.</returns>
        Task<ResponseType> HandleAsync (OperationInputType operationInput);

    }

}