using System.Reflection;

namespace SharedKernel.Application.Models.Abstractions.Operations {

    public class RegisteredHandlers {

        public Dictionary<Type, MethodInfo> SynchronousHandlers { get; }
        public Dictionary<Type, MethodInfo> AsynchronousHandlers { get; }

        public RegisteredHandlers (Dictionary<Type, MethodInfo> registeredSynchronousHandlers, Dictionary<Type, MethodInfo> registeredAsynchronousHandlers) {
            SynchronousHandlers = registeredSynchronousHandlers;
            AsynchronousHandlers = registeredAsynchronousHandlers;
        }

    }

}