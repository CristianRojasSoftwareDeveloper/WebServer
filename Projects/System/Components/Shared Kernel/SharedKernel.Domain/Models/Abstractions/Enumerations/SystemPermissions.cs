using SharedKernel.Domain.Models.Abstractions.Attributes;

namespace SharedKernel.Domain.Models.Abstractions.Enumerations {

    /// <summary>
    /// Define los permisos de acceso disponibles en el sistema utilizando una estructura de bits.
    /// Esta enumeración permite gestionar los distintos niveles de acceso que pueden ser asignados a los usuarios.
    /// </summary>
    /// <remarks>
    /// Los permisos están organizados en las siguientes categorías:
    /// <list type="bullet">
    /// <item><description>Permisos Genéricos (permisos 1-5)</description></item>
    /// <item><description>Gestión de Usuarios (permisos 6-15)</description></item>
    /// <item><description>Gestión de Roles (permisos 16-23)</description></item>
    /// <item><description>Gestión de Permisos de Acceso (permisos 24-29)</description></item>
    /// <item><description>Gestión de Registros del Sistema (permisos 30-35)</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Ejemplos de uso:
    /// <code>
    ///
    /// // Agregar un nuevo permiso a la lista
    /// permisos.Add(SystemPermissions.UpdateUser);
    ///
    /// // Crear una lista de permisos
    /// var permisos = new List<SystemPermissions> {
    ///     SystemPermissions.GetUsers,
    ///     SystemPermissions.RegisterUser,
    ///     SystemPermissions.AddRole
    /// };
    ///
    /// // Agregar múltiples permisos a la lista usando AddRange
    /// permisos.AddRange(new[] {
    ///     SystemPermissions.UpdateUser,
    ///     SystemPermissions.DeleteUserByID
    /// });
    ///
    /// // Verificar si un permiso específico está en la lista
    /// if (permisos.Contains(SystemPermissions.GetUsers)) {
    ///     // El usuario tiene permiso para listar usuarios
    /// }
    ///
    /// // Filtrar permisos que comienzan con 'Get'
    /// var permisosGet = permisos.Where(p => p.ToString().StartsWith("Get")).ToList();
    ///
    /// // Verificar si el usuario tiene todos los permisos necesarios para registrar un usuario y agregar un rol
    /// var requiredPermissions = new[] { SystemPermissions.RegisterUser, SystemPermissions.AddRole };
    /// if (requiredPermissions.All(rp => permisos.Contains(rp))) {
    ///     // El usuario tiene todos los permisos necesarios
    /// }
    ///
    /// // Contar cuántos permisos tiene el usuario
    /// int count = permisos.Count;
    /// Console.WriteLine($"El usuario tiene {count} permisos.");
    ///
    /// // Mostrar todos los permisos disponibles
    /// foreach (var permiso in permisos) {
    ///     Console.WriteLine(permiso);
    /// }
    /// </code>
    /// </example>
    public enum SystemPermissions {

        /// <summary>
        /// Representa ningún permiso.
        /// </summary>
        [Permission("Ningún permiso")]
        None = 0,

        #region Operaciones Genéricas

        #region Queries
        /// <summary>
        /// Permite obtener una lista paginada de entidades con filtrado y ordenamiento.
        /// </summary>
        [Permission("Permiso para obtener entidades")]
        GetEntities = 1,

        /// <summary>
        /// Permite obtener una entidad específica por su ID.
        /// </summary>
        [Permission("Permiso para obtener entidades por ID")]
        GetEntityByID = 2,

        #endregion

        #region Commands
        /// <summary>
        /// Permite crear una nueva entidad en el sistema.
        /// </summary>
        [Permission("Permiso para agregar entidades")]
        AddEntity = 3,

        /// <summary>
        /// Permite actualizar una entidad existente.
        /// </summary>
        [Permission("Permiso para actualizar entidades")]
        UpdateEntity = 4,

        /// <summary>
        /// Permite eliminar una entidad por su ID.
        /// </summary>
        [Permission("Permiso para eliminar entidades")]
        DeleteEntityByID = 5,
        #endregion

        #endregion

        #region Gestión de Usuarios

        #region Queries
        /// <summary>
        /// Permite obtener la lista de usuarios del sistema.
        /// </summary>
        [Permission("Permiso para listar usuarios")]
        GetUsers = 6,

        /// <summary>
        /// Permite obtener un usuario por su ID.
        /// </summary>
        [Permission("Permiso para obtener usuarios por ID")]
        GetUserByID = 7,

        /// <summary>
        /// Permite obtener un usuario por su nombre de usuario.
        /// </summary>
        [Permission("Permiso para obtener usuarios por nombre de usuario")]
        GetUserByUsername = 8,

        /// <summary>
        /// Permite obtener el usuario actual por su token JWT.
        /// </summary>
        [Permission("Permiso para obtener usuarios por token JWT")]
        GetUserByToken = 9,

        /// <summary>
        /// Permite autenticar un usuario con sus credenciales.
        /// </summary>
        [Permission("Permiso para autenticar usuarios")]
        AuthenticateUser = 10,
        #endregion

        #region Commands
        /// <summary>
        /// Permite registrar un nuevo usuario en el sistema.
        /// </summary>
        [Permission("Permiso para registrar usuarios")]
        RegisterUser = 11,

        /// <summary>
        /// Permite actualizar la información de un usuario.
        /// </summary>
        [Permission("Permiso para actualizar usuarios")]
        UpdateUser = 12,

        /// <summary>
        /// Permite eliminar un usuario por su ID.
        /// </summary>
        [Permission("Permiso para eliminar usuarios")]
        DeleteUserByID = 13,

        /// <summary>
        /// Permite asignar un rol a un usuario.
        /// </summary>
        [Permission("Permiso para asignar roles a usuarios")]
        AddRoleToUser = 14,

        /// <summary>
        /// Permite revocar un rol de un usuario.
        /// </summary>
        [Permission("Permiso para revocar roles de usuarios")]
        RemoveRoleFromUser = 15,
        #endregion

        #endregion

        #region Gestión de Roles

        #region Queries
        /// <summary>
        /// Permite obtener la lista de roles del sistema.
        /// </summary>
        [Permission("Permiso para listar roles")]
        GetRoles = 16,

        /// <summary>
        /// Permite obtener un rol por su ID.
        /// </summary>
        [Permission("Permiso para obtener roles por ID")]
        GetRoleByID = 17,

        /// <summary>
        /// Permite obtener los roles de un usuario.
        /// </summary>
        [Permission("Permiso para obtener roles de usuarios")]
        GetRolesByUserID = 18,
        #endregion

        #region Commands
        /// <summary>
        /// Permite crear un nuevo rol en el sistema.
        /// </summary>
        [Permission("Permiso para agregar roles")]
        AddRole = 19,

        /// <summary>
        /// Permite actualizar un rol existente.
        /// </summary>
        [Permission("Permiso para actualizar roles")]
        UpdateRole = 20,

        /// <summary>
        /// Permite eliminar un rol por su ID.
        /// </summary>
        [Permission("Permiso para eliminar roles")]
        DeleteRoleByID = 21,

        /// <summary>
        /// Permite asignar un permiso a un rol.
        /// </summary>
        [Permission("Permiso para asignar permisos a roles")]
        AddPermissionToRole = 22,

        /// <summary>
        /// Permite revocar un permiso de un rol.
        /// </summary>
        [Permission("Permiso para revocar permisos de roles")]
        RemovePermissionFromRole = 23,
        #endregion

        #endregion

        #region Gestión de Permisos de Acceso

        #region Queries
        /// <summary>
        /// Permite obtener la lista de permisos del sistema.
        /// </summary>
        [Permission("Permiso para listar permisos")]
        GetPermissions = 24,

        /// <summary>
        /// Permite obtener un permiso por su ID.
        /// </summary>
        [Permission("Permiso para obtener permisos por ID")]
        GetPermissionByID = 25,

        /// <summary>
        /// Permite obtener los permisos de un rol.
        /// </summary>
        [Permission("Permiso para obtener permisos de roles")]
        GetPermissionsByRoleID = 26,
        #endregion

        #region Commands
        /// <summary>
        /// Permite crear un nuevo permiso en el sistema.
        /// </summary>
        [Permission("Permiso para agregar permisos")]
        AddPermission = 27,

        /// <summary>
        /// Permite actualizar un permiso existente.
        /// </summary>
        [Permission("Permiso para actualizar permisos")]
        UpdatePermission = 28,

        /// <summary>
        /// Permite eliminar un permiso por su ID.
        /// </summary>
        [Permission("Permiso para eliminar permisos")]
        DeletePermissionByID = 29,
        #endregion

        #endregion

        #region Gestión de Registros del Sistema

        #region Queries
        /// <summary>
        /// Permite obtener los registros de log del sistema.
        /// </summary>
        [Permission("Permiso para obtener registros de log")]
        GetSystemLogs = 30,

        /// <summary>
        /// Permite obtener un registro de log por su ID.
        /// </summary>
        [Permission("Permiso para obtener registros de log por ID")]
        GetSystemLogByID = 31,

        /// <summary>
        /// Permite obtener los registros de log de un usuario.
        /// </summary>
        [Permission("Permiso para obtener registros de log de usuarios")]
        GetSystemLogsByUserID = 32,
        #endregion

        #region Commands
        /// <summary>
        /// Permite agregar un nuevo registro de log.
        /// </summary>
        [Permission("Permiso para agregar registros de log")]
        AddSystemLog = 33,

        /// <summary>
        /// Permite actualizar un registro de log existente.
        /// </summary>
        [Permission("Permiso para actualizar registros de log")]
        UpdateSystemLog = 34,

        /// <summary>
        /// Permite eliminar un registro de log por su ID.
        /// </summary>
        [Permission("Permiso para eliminar registros de log")]
        DeleteSystemLogByID = 35
        #endregion

        #endregion

    }

}