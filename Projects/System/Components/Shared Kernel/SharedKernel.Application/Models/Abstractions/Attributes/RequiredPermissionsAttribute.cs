using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Application.Models.Abstractions.Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class RequiredPermissionsAttribute : Attribute {

        public IEnumerable<SystemPermissions> Permissions { get; }

        public RequiredPermissionsAttribute (params SystemPermissions[] permissions)
            => Permissions = permissions != null && permissions.Length != 0 ? permissions : [SystemPermissions.None];

    }

}