namespace SharedKernel.Application.Models.Abstractions.Attributes {

    /// <summary>
    /// Atributo que marca un método como manejador de una operación.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class OperationHandlerAttribute : Attribute { }

}