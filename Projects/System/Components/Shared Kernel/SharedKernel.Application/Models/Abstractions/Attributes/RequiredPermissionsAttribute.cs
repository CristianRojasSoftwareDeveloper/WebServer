using SharedKernel.Application.Models.Abstractions.Enumerations;

namespace SharedKernel.Application.Models.Abstractions.Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class RequiredPermissionsAttribute : Attribute {

        public IEnumerable<Permissions> Permissions { get; }

        public RequiredPermissionsAttribute (params Permissions[] permissions)
            => Permissions = permissions != null && permissions.Length != 0 ? permissions : [Enumerations.Permissions.None];

    }

}