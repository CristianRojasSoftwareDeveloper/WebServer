using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Enumerations {

    /// <summary>
    /// Enumeración que contiene todos los roles de usuario disponibles en el sistema.
    /// </summary>
    public enum DefaultRoles {

        /// <summary>
        /// Rol de administrador del sistema con acceso total.
        /// </summary>
        [Role("Rol de administrador del sistema con acceso total", permissions: [
            Permissions.AddEntity,
            Permissions.GetEntities,
            Permissions.GetEntityByID,
            Permissions.UpdateEntity,
            Permissions.DeleteEntityByID,
            Permissions.AddRoleToUser,
            Permissions.RemoveRoleFromUser,
            Permissions.AddPermissionToRole,
            Permissions.RemovePermissionFromRole
        ])]
        SystemAdministrator = 1,

        /// <summary>
        /// Rol de administrador de usuarios con capacidad de gestionar cuentas.
        /// </summary>
        [Role("Rol de administrador de usuarios con capacidad de gestionar cuentas", permissions: [
            Permissions.GetEntities,
            Permissions.GetEntityByID,
            Permissions.RegisterUser,
            Permissions.AddRoleToUser,
            Permissions.RemoveRoleFromUser
        ])]
        UserAdministrator = 2,

        /// <summary>
        /// Rol de usuario estándar del sistema.
        /// </summary>
        [Role("Rol de usuario estándar del sistema", permissions: [
            Permissions.GetUserByID,
            Permissions.UpdateUser,
            Permissions.DeleteUserByID,
            Permissions.GetUserByToken,
            Permissions.GetUserByUsername
        ])]
        User = 3,

        /// <summary>
        /// Rol de invitado con acceso limitado al sistema.
        /// </summary>
        [Role("Rol de usuario invitado (Usuario no autenticado)", permissions: [
            Permissions.RegisterUser,
            Permissions.AuthenticateUser
        ])]
        Guest = 4

    }

}