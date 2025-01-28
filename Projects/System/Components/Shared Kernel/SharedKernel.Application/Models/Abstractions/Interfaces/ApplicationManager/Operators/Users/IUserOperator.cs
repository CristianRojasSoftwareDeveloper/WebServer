using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Commands;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.Use_Cases.Queries;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users {

    /// <summary>
    /// Interfaz para operaciones relacionadas con la gestión de los usuarios del sistema.
    /// </summary>
    public interface IUserOperator : IReflexiveOperator {

        #region Métodos asíncronos

        /// <summary>
        /// Autentica un usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de autenticación que contiene las credenciales del usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el token JWT si la autenticación es exitosa, o un Error si falla.</returns>
        Task<Response<string>> AuthenticateUser (IAuthenticateUser_Query query);

        /// <summary>
        /// Registra un usuario de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene los datos del usuario a registrar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario registrado, o un Error si la operación falla.</returns>
        Task<Response<User>> RegisterUser (IRegisterUser_Command command);

        /// <summary>
        /// Obtiene todos los usuarios de forma asíncrona.
        /// </summary>
        /// <param name="query">Opcional. La consulta para filtrar los usuarios.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene una lista de usuarios, o un Error si la operación falla.</returns>
        Task<Response<List<User>>> GetUsers (IGetUsers_Query query);

        /// <summary>
        /// Obtiene un usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del usuario a obtener.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario encontrado, o un Error si no se encuentra.</returns>
        Task<Response<User>> GetUserByID (IGetUserByID_Query query);

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el nombre de usuario a buscar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario encontrado, o un Error si no se encuentra.</returns>
        Task<Response<User>> GetUserByUsername (IGetUserByUsername_Query query);

        /// <summary>
        /// Obtiene un usuario asociado a un token JWT de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el token JWT.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario asociado al token, o un Error si no se encuentra.</returns>
        Task<Response<User>> GetUserByToken (IGetUserByToken_Query query);

        /// <summary>
        /// Actualiza un usuario existente de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene los datos actualizados del usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el usuario actualizado, o un Error si la operación falla.</returns>
        Task<Response<User>> UpdateUser (IUpdateUser_Command command);

        /// <summary>
        /// Elimina un usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del usuario a eliminar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si la operación fue exitosa, o un Error si la operación falla.</returns>
        Task<Response<bool>> DeleteUserByID (IDeleteUserByID_Command command);

        /// <summary>
        /// Asigna un rol a un usuario de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del usuario y el ID del rol a asignar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que contiene el objeto RolesAssignedToUser, o un Error si la operación falla.</returns>
        Task<Response<RoleAssignedToUser>> AddRoleToUser (IAddRoleToUser_Command command);

        /// <summary>
        /// Remueve un rol de un usuario de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID del usuario y el ID del rol a remover.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un resultado que indica si la operación fue exitosa, o un Error si la operación falla.</returns>
        Task<Response<bool>> RemoveRoleFromUser (IRemoveRoleFromUser_Command command);

        #endregion

    }

}