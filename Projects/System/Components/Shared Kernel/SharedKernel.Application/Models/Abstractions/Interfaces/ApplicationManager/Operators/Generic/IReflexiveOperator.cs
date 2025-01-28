using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic {

    /// <summary>
    /// Interfaz para un operador reflexivo que gestiona las operaciones mediante reflexión.
    /// </summary>
    public interface IReflexiveOperator {

        /// <summary>
        /// Ejecuta un manejador de operación asíncrono para la operación especificada.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de operación a ejecutar.</typeparam>
        /// <typeparam name="ResponseType">El tipo de respuesta esperado de la operación.</typeparam>
        /// <param name="operation">La operación a ejecutar.</param>
        /// <param name="tokenClaims">Las reclamaciones del token de autenticación del usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene la respuesta de la operación.</returns>
        /// <remarks>
        /// Este método realiza las siguientes acciones:
        /// 1. **Validación de permisos:** Valida que el usuario tenga los permisos necesarios para ejecutar la operación.
        /// 2. **Obtención de manejadores:** Obtiene los manejadores de operaciones registrados para el operador actual.
        /// 3. **Búsqueda del manejador:** Busca el manejador correspondiente al tipo de operación especificado.
        /// 4. **Invocación del manejador:** Invoca el manejador encontrado utilizando la operación como argumento. 
        ///    - El manejador se invoca de forma dinámica utilizando la reflexión.
        ///    - Se espera que el manejador sea un método asíncrono que devuelva una `Task<Response<ResponseType>>`.
        /// 5. **Retorno del resultado:** Devuelve el resultado de la operación como una tarea.
        /// 
        /// **Manejo de errores:**
        /// - Si no se encuentra un manejador registrado para el tipo de operación especificado, se lanza una excepción `InvalidOperationException`.
        /// </remarks>
        /// <exception cref="UnauthorizedAccessException">Se lanza si el usuario no tiene los permisos necesarios para ejecutar la operación.</exception>
        /// <exception cref="InvalidOperationException">Se lanza si no se encuentra un manejador registrado para el tipo de operación especificado.</exception>
        Task<Response<ResponseType>> ExecuteHandler<OperationType, ResponseType> (OperationType operation, TokenClaims tokenClaims) where OperationType : IOperation;

    }

}