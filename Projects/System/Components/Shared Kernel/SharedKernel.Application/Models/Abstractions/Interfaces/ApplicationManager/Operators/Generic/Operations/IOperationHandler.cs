namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations {

    /// <summary>
    /// Interfaz para manejar operaciones de forma asíncrona.
    /// </summary>
    /// <typeparam name="OperationInputType">Tipo de entrada de la operación.</typeparam>
    /// <typeparam name="ResponseType">Tipo de resultado de la operación.</typeparam>
    public interface IOperationHandler<OperationInputType, ResponseType> where OperationInputType : IOperation {

        /// <summary>
        /// Ejecuta la operación de forma asíncrona.
        /// </summary>
        /// <param name="operationInput">Entrada de la operación.</param>
        /// <returns>Una tarea que representa el resultado de la operación.</returns>
        Task<ResponseType> Handle (OperationInputType operationInput);

    }

}
