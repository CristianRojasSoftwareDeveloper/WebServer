using ConsoleApplication.Utils;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Commands;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.CRUD.Queries;
using SharedKernel.Application.Models.Abstractions.Operations.Requests.Operators.Users.UseCases.Queries;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Domain.Models.Entities.Users;
using System.Net;

namespace ConsoleApplication.Layers {

    internal class Main_Operators_Layer {

        #region Flujo de operaciones de prueba de la capa de operadores del sistema (también llamada capa de aplicación/casos de uso en Arquitectura Limpia)
        /// <summary>
        /// Punto de entrada principal de la aplicación de consola.
        /// </summary>
        /// <param name="args">Argumentos de la línea de comandos.</param>
        internal static void ExecuteTestFlow (IApplicationManager applicationManager) {

            Printer.PrintLine($"\n{"Iniciando el flujo de operaciones de prueba de la capa de operadores del sistema (también llamada capa de aplicación/casos de uso en Arquitectura Limpia)".Underline()}");

            #region Paso 1: Registrar un nuevo usuario
            Printer.PrintLine($"\n{"1. Registrando un nuevo usuario [applicationManager.UserOperator.RegisterUser(new RegisterUser_Command(newUser, roleAssignedToUser))]".Underline()}");

            var newUser = new User {
                Username = "CristianDeveloper",
                Password = "secret-key",
                Name = "Cristian Rojas",
                Email = "cristian.rojas.software.developer@gmail.com"
            };

            // Agregando roles al usuario (DefaultRoles por defecto: [ 1=Administrator, 2=Moderator, 3=User, 4=Guest ])
            int[] roleAssignedToUser = [1, 2, 3];
            var registerUser_Response = applicationManager.UserOperator.RegisterUser(new RegisterUser_Command(newUser, roleAssignedToUser));
            if (registerUser_Response == null || registerUser_Response.Body == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la operación de registro de usuario en la base de datos no es válida");

            var registeredUser = registerUser_Response.Body;
            Printer.PrintLine($"Se ha registrado el usuario {registeredUser.Username} correctamente");
            registeredUser.PrintUser();
            #endregion

            #region Paso 2: Autenticar el usuario registrado
            Printer.PrintLine($"\n{"2. Autenticando usuario [applicationManager.UserOperator.AuthenticateUser(new AuthenticateUser_Query(newUser.Username, newUser.Password))]".Underline()}");

            var username = "CristianDeveloper";
            var password = "secret-key";
            Printer.PrintLine($"\n- Usuario: {username}\n- Contraseña: {password}");

            var accessToken = applicationManager.UserOperator.AuthenticateUser(new AuthenticateUser_Query(newUser.Username, newUser.Password)).Body;

            Printer.PrintLine($"\n- Token: {accessToken}");
            Printer.PrintLine($"\n- Token desencriptado:\n{applicationManager.AuthService.ValidateToken(accessToken)}");
            #endregion

            #region Paso 3: Consultar el usuario registrado según su ID
            Printer.PrintLine($"\n{"3. Consultando el usuario registrado por su ID [applicationManager.UserOperator.GetUserByID(new GetUserByID_Query((int) registeredUser.ID!))]".Underline()}");
            var foundUser = applicationManager.UserOperator.GetUserByID(new GetUserByID_Query((int) registeredUser.ID!)).Body;
            if (foundUser != null) {
                Printer.PrintLine("Usuario encontrado:");
                foundUser.PrintUser();
            } else
                Printer.PrintLine($"No se encontró ningún usuario con el ID: {(int) registeredUser.ID}");
            #endregion

            #region Paso 4: Actualizar el usuario registrado
            if (foundUser != null) {
                Printer.PrintLine($"\n{"4. Actualizando el usuario registrado [applicationManager.UserOperator.UpdateUser(new UpdateUser_Command(new User { ID = foundUser.ID, Name = \"Cristian Rojas Arredondo\" }))]".Underline()}");
                var updatedUserResponse = applicationManager.UserOperator.UpdateUser(new UpdateUser_Command(new User { ID = foundUser.ID, Name = "Cristian Rojas Arredondo" }));
                if (updatedUserResponse != null && updatedUserResponse.Body != null) {
                    Printer.PrintLine("Usuario actualizado correctamente:");
                    updatedUserResponse.Body.PrintUser();
                } else
                    Printer.PrintLine($"No se pudo actualizar el usuario con el ID: {foundUser.ID}");
            }
            #endregion

            #region Paso 5: Consultar todos los usuarios después de la actualización del usuario registrado
            Printer.PrintLine($"\n{"5. Consultando todos los usuarios después de la actualización [applicationManager.UserOperator.GetUsers()]".Underline()}");
            var getUsersResponse = applicationManager.UserOperator.GetUsers();
            if (getUsersResponse == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la consulta para obtener los usuarios de la base de datos no es válida");
            var usersAfterUpdate = getUsersResponse.Body;
            if (usersAfterUpdate != null && usersAfterUpdate.Count > 0) {
                Printer.PrintLine("Lista de usuarios:");
                foreach (var user in usersAfterUpdate)
                    user.PrintUser();
            } else
                Printer.PrintLine("No hay usuarios en la base de datos");
            #endregion

            #region Paso 6: Eliminar el usuario registrado según su ID
            Printer.PrintLine($"\n{"6. Eliminando el usuario registrado según su ID [applicationManager.UserOperator.DeleteUserByID(new DeleteUserByID_Command((int) registeredUser.ID))]".Underline()}");
            var deleteUserByID_Response = applicationManager.UserOperator.DeleteUserByID(new DeleteUserByID_Command((int) registeredUser.ID));
            if (deleteUserByID_Response == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la operación de eliminación de un usuario de la base de datos según su ID no es válida");
            var isDeleted = deleteUserByID_Response.Body;
            if (isDeleted)
                Printer.PrintLine($"Usuario con ID: {registeredUser.ID} eliminado correctamente");
            else
                Printer.PrintLine($"No se pudo eliminar el usuario con el ID: {registeredUser.ID}");
            #endregion

            #region Paso 7: Consultar todos los usuarios nuevamente después de la eliminación del usuario registrado
            Printer.PrintLine($"\n{"7. Consultando todos los usuarios después de la eliminación [applicationManager.UserOperator.GetUsers()]".Underline()}");
            var remainingUsers_Response = applicationManager.UserOperator.GetUsers();
            if (remainingUsers_Response == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la consulta de todos los usuarios de la base de datos no es válida");
            var remainingUsers = remainingUsers_Response.Body;
            if (remainingUsers != null && remainingUsers.Count > 0) {
                Printer.PrintLine("Lista de usuarios después de la eliminación:");
                foreach (var user in remainingUsers)
                    user.PrintUser();
            } else
                Printer.PrintLine("No hay usuarios en la base de datos");
            #endregion

            Printer.PrintLine($"\n{"Ha finalizado el flujo de operaciones de prueba de la capa de operadores del sistema (también llamada capa de aplicación/casos de uso en Arquitectura Limpia)".Underline()}");

        }
        #endregion

    }

}