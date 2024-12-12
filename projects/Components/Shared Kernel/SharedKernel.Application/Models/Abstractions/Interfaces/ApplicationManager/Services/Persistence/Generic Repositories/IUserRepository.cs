using SharedKernel.Domain.Models.Entities.Users;
using System.Linq.Expressions;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    /// <summary>
    /// Define las operaciones CRUD específicas para la entidad Usuario.
    /// </summary>
    public interface IUserRepository {

        #region Métodos síncronos específicos de usuario

        /// <summary>
        /// Agrega un nuevo usuario al repositorio.
        /// </summary>
        /// <param name="newUser">El usuario a agregar.</param>
        /// <returns>El usuario agregado.</returns>
        User AddUser (User newUser);

        /// <summary>
        /// Obtiene todos los usuarios del repositorio.
        /// </summary>
        /// <returns>Una colección de usuarios.</returns>
        List<User> GetUsers ();

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="userID">El ID del usuario.</param>
        /// <returns>El usuario encontrado o null si no existe.</returns>
        User GetUserByID (int userID);

        User GetUserByUsername (string username);

        User FirstUserOrDefault (Expression<Func<User, bool>> predicate);

        /// <summary>
        /// Actualiza un usuario existente por su ID.
        /// </summary>
        /// <param name="updatedUser">El usuario actualizado.</param>
        /// <returns>El usuario actualizado o null si no se encontró.</returns>
        User UpdateUser (User updatedUser);

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="userID">El ID del usuario a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, false si no se encontró el usuario.</returns>
        bool DeleteUserByID (int userID);

        #endregion

        #region Métodos asíncronos específicos de usuario

        /// <summary>
        /// Agrega un nuevo usuario al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newUser">El usuario a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el usuario agregado como resultado.</returns>
        Task<User> AddUserAsync (User newUser);

        /// <summary>
        /// Obtiene todos los usuarios del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de usuarios como resultado.</returns>
        Task<List<User>> GetUsersAsync ();

        /// <summary>
        /// Obtiene un usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="userID">El ID del usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el usuario encontrado o null si no existe como resultado.</returns>
        Task<User> GetUserByIDAsync (int userID);

        Task<User> GetUserByUsernameAsync (string username);

        Task<User> FirstUserOrDefaultAsync (Expression<Func<User, bool>> predicate);

        /// <summary>
        /// Actualiza un usuario existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="updatedUser">El usuario actualizado.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el usuario actualizado o null si no se encontró como resultado.</returns>
        Task<User> UpdateUserAsync (User updatedUser);

        /// <summary>
        /// Elimina un usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="userID">El ID del usuario a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<bool> DeleteUserByIDAsync (int userID);

        #endregion

    }

}