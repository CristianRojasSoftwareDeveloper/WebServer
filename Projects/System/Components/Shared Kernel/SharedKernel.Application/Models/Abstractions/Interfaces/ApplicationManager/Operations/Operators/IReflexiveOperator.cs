using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators {

    /// <summary>
    /// Interfaz para un operador reflexivo que gestiona las operaciones mediante reflexión.
    /// </summary>
    public interface IReflexiveOperator {

        /// <summary>
        /// Ejecuta el manejador síncrono registrado para el tipo de solicitud especificado.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de solicitud a ejecutar.</typeparam>
        /// <typeparam name="ResponseType">El tipo de respuesta esperada.</typeparam>
        /// <param name="operation">La solicitud a ejecutar.</param>
        /// <param name="tokenClaims">Metadata extraída desde el token de acceso proporcionado por el usuario.</param>
        /// <returns>La respuesta del manejador.</returns>
        /// <exception cref="InvalidOperationException">Si no hay un manejador registrado para el tipo de solicitud.</exception>
        /// <exception cref="ArgumentNullException">Si la solicitud es nula.</exception>
        Response<ResponseType> ExecuteSynchronousHandler<OperationType, ResponseType> (OperationType operation, TokenClaims tokenClaims) where OperationType : Operation;

        /// <summary>
        /// Ejecuta el manejador asíncrono registrado para el tipo de solicitud especificado.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de solicitud a ejecutar.</typeparam>
        /// <typeparam name="ResponseType">El tipo de respuesta esperada.</typeparam>
        /// <param name="operation">La solicitud a ejecutar.</param>
        /// <param name="tokenClaims">Metadata extraída desde el token de acceso proporcionado por el usuario.</param>
        /// <returns>La tarea que representa la operación asíncrona con la respuesta del manejador.</returns>
        /// <exception cref="InvalidOperationException">Si no hay un manejador registrado para el tipo de solicitud.</exception>
        /// <exception cref="ArgumentNullException">Si la solicitud es nula.</exception>
        Task<Response<ResponseType>> ExecuteAsynchronousHandler<OperationType, ResponseType> (OperationType operation, TokenClaims tokenClaims) where OperationType : Operation;

    }

}