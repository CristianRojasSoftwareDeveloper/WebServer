using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Queries;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operations.Operators {

    /// <summary>
    /// Interfaz para operaciones relacionadas con la gestión de los usuarios del sistema.
    /// </summary>
    public interface IUserOperator {

        #region Métodos síncronos

        /// <summary>
        /// Autentica un usuario de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta de autenticación que contiene las credenciales del usuario.</param>
        /// <returns>Un resultado que contiene el token JWT si la autenticación es exitosa, o un Error si falla.</returns>
        Response<string> AuthenticateUser (AuthenticateUser_Query query);

        /// <summary>
        /// Registra un usuario de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene los datos del usuario a registrar.</param>
        /// <returns>Un resultado que contiene el usuario registrado, o un Error si la operación falla.</returns>
        Response<User> RegisterUser (RegisterUser_Command command);

        /// <summary>
        /// Obtiene todos los usuarios de forma síncrona.
        /// </summary>
        /// <param name="query">Opcional. La consulta para filtrar los usuarios.</param>
        /// <returns>Un resultado que contiene una lista de usuarios, o un Error si la operación falla.</returns>
        Response<List<User>> GetUsers (GetUsers_Query query = null);

        /// <summary>
        /// Obtiene un usuario por su ID de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del usuario a obtener.</param>
        /// <returns>Un resultado que contiene el usuario encontrado, o un Error si no se encuentra.</returns>
        Response<User> GetUserByID (GetUserByID_Query query);

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el nombre de usuario a buscar.</param>
        /// <returns>Un resultado que contiene el usuario encontrado, o un Error si no se encuentra.</returns>
        Response<User> GetUserByUsername (GetUserByUsername_Query query);

        /// <summary>
        /// Obtiene un usuario asociado a un token JWT de forma síncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el token JWT.</param>
        /// <returns>Un resultado que contiene el usuario asociado al token, o un Error si no se encuentra.</returns>
        Response<User> GetUserByToken (GetUserByToken_Query query);

        /// <summary>
        /// Actualiza un usuario existente de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene los datos actualizados del usuario.</param>
        /// <returns>Un resultado que contiene el usuario actualizado, o un Error si la operación falla.</returns>
        Response<User> UpdateUser (UpdateUser_Command command);

        /// <summary>
        /// Elimina un usuario por su ID de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del usuario a eliminar.</param>
        /// <returns>Un resultado que indica si la operación fue exitosa, o un Error si la operación falla.</returns>
        Response<bool> DeleteUserByID (DeleteUserByID_Command command);

        /// <summary>
        /// Asigna un rol a un usuario de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del usuario y el ID del rol a asignar.</param>
        /// <returns>Un resultado que contiene el objeto RolesAssignedToUser, o un Error si la operación falla.</returns>
        Response<RoleAssignedToUser> AddRoleToUser (AddRoleToUser_Command command);

        /// <summary>
        /// Remueve un rol de un usuario de forma síncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del usuario y el ID del rol a remover.</param>
        /// <returns>Un resultado que indica si la operación fue exitosa, o un Error si la operación falla.</returns>
        Response<bool> RemoveRoleFromUser (RemoveRoleFromUser_Command command);

        #endregion

        #region Métodos asíncronos

        /// <summary>
        /// Autentica un usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de autenticación que contiene las credenciales del usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el token JWT si la autenticación es exitosa, o un Error si falla.</returns>
        Task<Response<string>> AuthenticateUserAsync (AuthenticateUser_Query query);

        /// <summary>
        /// Registra un usuario de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene los datos del usuario a registrar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario registrado, o un Error si la operación falla.</returns>
        Task<Response<User>> RegisterUserAsync (RegisterUser_Command command);

        /// <summary>
        /// Obtiene todos los usuarios de forma asíncrona.
        /// </summary>
        /// <param name="query">Opcional. La consulta para filtrar los usuarios.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una lista de usuarios, o un Error si la operación falla.</returns>
        Task<Response<List<User>>> GetUsersAsync (GetUsers_Query query = null);

        /// <summary>
        /// Obtiene un usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del usuario a obtener.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario encontrado, o un Error si no se encuentra.</returns>
        Task<Response<User>> GetUserByIDAsync (GetUserByID_Query query);

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el nombre de usuario a buscar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario encontrado, o un Error si no se encuentra.</returns>
        Task<Response<User>> GetUserByUsernameAsync (GetUserByUsername_Query query);

        /// <summary>
        /// Obtiene un usuario asociado a un token JWT de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el token JWT.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario asociado al token, o un Error si no se encuentra.</returns>
        Task<Response<User>> GetUserByTokenAsync (GetUserByToken_Query query);

        /// <summary>
        /// Actualiza un usuario existente de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene los datos actualizados del usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario actualizado, o un Error si la operación falla.</returns>
        Task<Response<User>> UpdateUserAsync (UpdateUser_Command command);

        /// <summary>
        /// Elimina un usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del usuario a eliminar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si la operación fue exitosa, o un Error si la operación falla.</returns>
        Task<Response<bool>> DeleteUserByIDAsync (DeleteUserByID_Command command);

        /// <summary>
        /// Asigna un rol a un usuario de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del usuario y el ID del rol a asignar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el objeto RolesAssignedToUser, o un Error si la operación falla.</returns>
        Task<Response<RoleAssignedToUser>> AddRoleToUserAsync (AddRoleToUser_Command command);

        /// <summary>
        /// Remueve un rol de un usuario de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del usuario y el ID del rol a remover.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si la operación fue exitosa, o un Error si la operación falla.</returns>
        Task<Response<bool>> RemoveRoleFromUserAsync (RemoveRoleFromUser_Command command);

        #endregion

    }

}