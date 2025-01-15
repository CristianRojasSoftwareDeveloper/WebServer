using SharedKernel.Application.Models.Abstractions.Enumerations;

namespace SharedKernel.Application.Utils.Extensions {

    public static class PermissionsExtensions {

        public static bool HasPermission (this IEnumerable<Permissions> permissions, Permissions permission) =>
            permissions != null && permissions.Contains(permission);

    }

}