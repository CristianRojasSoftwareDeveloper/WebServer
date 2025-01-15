namespace SharedKernel.Application.Models.Abstractions.Attributes {

    /// <summary>
    /// Atributo que establece la asociación entre una operación y el operador responsable de procesarla.
    /// Este atributo permite definir de manera declarativa qué interfaz de operador es responsable
    /// de manejar cada operación específica en el sistema.
    /// </summary>
    /// <remarks>
    /// Este atributo es fundamental para el enrutamiento automático de operaciones, ya que permite:
    /// - Identificar el operador responsable de cada operación sin necesidad de búsquedas reflexivas
    /// - Establecer una relación clara y directa entre operaciones y sus manejadores
    /// - Validar en tiempo de compilación la existencia del operador asociado
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class AssociatedOperatorAttribute : Attribute {

        /// <summary>
        /// Obtiene el tipo de operador asociado que debe procesar la operación.
        /// </summary>
        public Type OperatorType { get; }

        /// <summary>
        /// Inicializa una nueva instancia del atributo AssociatedOperator.
        /// </summary>
        /// <param name="operatorType">La interfaz del operador que procesará la operación. 
        /// Debe ser una interfaz que termine en "Operator".</param>
        /// <exception cref="ArgumentException">Se lanza cuando el tipo proporcionado no es una interfaz de operador válida.</exception>
        public AssociatedOperatorAttribute (Type operatorType) {
            if (!operatorType.IsInterface || !operatorType.Name.EndsWith("Operator"))
                throw new ArgumentException("El tipo debe ser una interfaz de operador (debe terminar en 'Operator')", nameof(operatorType));

            OperatorType = operatorType;
        }

    }

}