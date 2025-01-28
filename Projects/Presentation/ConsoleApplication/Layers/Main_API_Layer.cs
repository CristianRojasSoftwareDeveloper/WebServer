using ConsoleApplication.Utils;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Domain.Models.Entities.Users;
using System.Net;
using Users.Application.Operators.Users.Operations.CRUD.Commands.DeleteUserByID;
using Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using Users.Application.Operators.Users.Operations.CRUD.Commands.UpdateUser;
using Users.Application.Operators.Users.Operations.CRUD.Queries.GetUserByID;
using Users.Application.Operators.Users.Operations.CRUD.Queries.GetUsers;
using Users.Application.Operators.Users.Operations.Use_Cases.Queries.AuthenticateUser;

namespace ConsoleApplication.Layers {

    internal class Main_API_Layer {

        #region Flujo de operaciones de prueba de la capa API del sistema [ApplicationManager.Execute_Request->_Operator->Command/Query_Handler->_Service]
        /// <summary>
        /// Punto de entrada principal de la aplicación de consola.
        /// </summary>
        /// <param name="args">Argumentos de la línea de comandos.</param>
        internal static async Task ExecuteTestFlow (IApplicationManager applicationManager) {

            Printer.PrintLine($"\n{"Iniciando el flujo de operaciones de prueba de la capa API del sistema [ApplicationManager.ExecuteOperation->_Operator->_Command/_Query_Handler->_Service]".Underline()}");

            #region Paso 0: Autenticar al usuario administrador
            Printer.PrintLine($"\n{"0. Autenticando usuario [applicationManager.ExecuteOperation<AuthenticateUser_Query, string>(new AuthenticateUser_Query(administrator.Username, administrator.Password))]".Underline()}");

            (string Username, string Password) administrator = ("CristianSoftwareDeveloper", "secret-key");
            var authenticateAdmin_Response = await applicationManager.ExecuteOperation<AuthenticateUser_Query, string>(new AuthenticateUser_Query(administrator.Username, administrator.Password));
            if (authenticateAdmin_Response == null || string.IsNullOrWhiteSpace(authenticateAdmin_Response.Body))
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la operación de autenticación del administrador no es válida");

            var accessToken = authenticateAdmin_Response.Body;

            Printer.PrintLine($"El administrador ha sido autenticado exitosamente.");
            #endregion

            #region Paso 1: Registrar un nuevo usuario
            Printer.PrintLine($"\n{"1. Registrando un nuevo usuario [applicationManager.ExecuteOperation<RegisterUser_Command, Entity>(new RegisterUser_Command(newUser, roleAssignedToUser))]".Underline()}");

            const string decryptedPassword = "secret-key";
            var newUser = new User {
                Username = "JohnDoe",
                Email = "john.doe@gmail.com",
                Name = "John",
                Password = decryptedPassword
            };

            // Agregando roles al usuario (DefaultRoles por defecto: [ 1=SystemAdministrator, 2=UserAdministrator, 3=Entity, 4=Guest ])
            int[] roleAssignedToUser = [3];
            var registerUser_Response = await applicationManager.ExecuteOperation<RegisterUser_Command, User>(new RegisterUser_Command(newUser, roleAssignedToUser), accessToken);
            if (registerUser_Response == null || registerUser_Response.Body == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la operación de registro de usuario en la base de datos no es válida");

            var registeredUser = registerUser_Response.Body;
            Printer.PrintLine($"Se ha registrado el usuario «{registeredUser.Username}» exitosamente.");
            registeredUser.PrintUser();
            #endregion

            #region Paso 2: Autenticar el usuario registrado
            Printer.PrintLine($"\n{"2. Autenticando usuario [applicationManager.ExecuteOperation<AuthenticateUser_Query, string>(new AuthenticateUser_Query(newUser.Username, newUser.Password))]".Underline()}");

            var authenticateUser_Response = await applicationManager.ExecuteOperation<AuthenticateUser_Query, string>(new AuthenticateUser_Query(newUser.Username, decryptedPassword), accessToken);
            if (authenticateUser_Response == null || string.IsNullOrWhiteSpace(authenticateUser_Response.Body))
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la operación de autenticación del usuario no es válida");

            var generatedAccessToken = authenticateUser_Response.Body;
            var tokenClaims = applicationManager.AuthService.ValidateToken(generatedAccessToken);
            Printer.PrintLine($"El usuario «{newUser.Username}» ha sido autenticado exitosamente.");
            Printer.PrintLine($"{tokenClaims}");
            #endregion

            #region Paso 3: Consultar el usuario registrado según su ID
            Printer.PrintLine($"{"3. Consultando el usuario registrado por su ID [applicationManager.ExecuteOperation<GetUserByID_Query, Entity>(new GetUserByID_Query((int)registeredUser.ID!))]".Underline()}");
            var getUserByID_Response = await applicationManager.ExecuteOperation<GetUserByID_Query, User>(new GetUserByID_Query((int) registeredUser.ID!, true), accessToken);
            if (getUserByID_Response == null || getUserByID_Response.Body == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la consulta del usuario por ID no es válida");

            var foundUser = getUserByID_Response.Body;
            if (foundUser != null) {
                Printer.PrintLine("Usuario encontrado:");
                foundUser.PrintUser();
            } else {
                Printer.PrintLine($"No se encontró ningún usuario con el ID: {registeredUser.ID}");
            }
            #endregion

            #region Paso 4: Actualizar el usuario registrado
            if (foundUser != null) {
                Printer.PrintLine($"\n{"4. Actualizando el usuario registrado [applicationManager.ExecuteOperation<UpdateUser_Command, Entity>(new UpdateUser_Command(new Entity { ID = foundUser.ID, Name = 'Cristian Rojas Arredondo' }))]".Underline()}");
                var updateUser_Response = await applicationManager.ExecuteOperation<UpdateUser_Command, User>(new UpdateUser_Command(new User { ID = foundUser.ID, Name = "Cristian Rojas Arredondo" }.AsPartial(user => user.Name)), accessToken);
                if (updateUser_Response == null || updateUser_Response.Body == null)
                    throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la operación de actualización del usuario no es válida");

                var updatedUser = updateUser_Response.Body;
                Printer.PrintLine("Usuario actualizado exitosamente:");
                updatedUser.PrintUser();
            }
            #endregion

            #region Paso 5: Consultar todos los usuarios después de la actualización del usuario registrado
            Printer.PrintLine($"\n{"5. Consultando todos los usuarios después de la actualización [applicationManager.ExecuteOperation<GetUsers_Query, List<Entity>>(new GetUsers_Query())]".Underline()}");
            var getUsers_Response = await applicationManager.ExecuteOperation<GetUsers_Query, List<User>>(new GetUsers_Query(), accessToken);
            if (getUsers_Response == null || getUsers_Response.Body == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la consulta para obtener los usuarios de la base de datos no es válida");

            var usersAfterUpdate = getUsers_Response.Body;
            if (usersAfterUpdate != null && usersAfterUpdate.Count > 0) {
                Printer.PrintLine("Lista de usuarios:");
                foreach (var user in usersAfterUpdate)
                    user.PrintUser();
            } else {
                Printer.PrintLine("No hay usuarios en la base de datos");
            }
            #endregion

            #region Paso 6: Eliminar el usuario registrado según su ID
            Printer.PrintLine($"\n{"6. Eliminando el usuario registrado según su ID [applicationManager.ExecuteOperation<DeleteUserByID_Command, bool>(new DeleteUserByID_Command((int)registeredUser.ID))]".Underline()}");
            var deleteUserByID_Response = await applicationManager.ExecuteOperation<DeleteUserByID_Command, bool>(new DeleteUserByID_Command((int) foundUser.ID!), accessToken);
            if (deleteUserByID_Response == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la operación de eliminación de un usuario de la base de datos según su ID no es válida");

            var isDeleted = deleteUserByID_Response.Body;
            if (isDeleted)
                Printer.PrintLine($"Usuario con ID: {registeredUser.ID} eliminado exitosamente");
            else
                Printer.PrintLine($"No se pudo eliminar el usuario con el ID: {registeredUser.ID}");
            #endregion

            #region Paso 7: Consultar todos los usuarios nuevamente después de la eliminación del usuario registrado
            Printer.PrintLine($"\n{"7. Consultando todos los usuarios después de la eliminación [applicationManager.ExecuteOperation<GetUsers_Query, List<Entity>>(new GetUsers_Query())]".Underline()}");
            var remainingUsers_Response = await applicationManager.ExecuteOperation<GetUsers_Query, List<User>>(new GetUsers_Query(), accessToken);
            if (remainingUsers_Response == null || remainingUsers_Response.Body == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la consulta de todos los usuarios de la base de datos no es válida");

            var remainingUsers = remainingUsers_Response.Body;
            if (remainingUsers != null && remainingUsers.Count > 0) {
                Printer.PrintLine("Lista de usuarios después de la eliminación:");
                foreach (var user in remainingUsers)
                    user.PrintUser();
            } else {
                Printer.PrintLine("No hay usuarios en la base de datos");
            }
            #endregion

            Printer.PrintLine($"\n{"Ha finalizado el flujo de operaciones de prueba de la capa API del sistema [ApplicationManager.Execute_Request->_Operator->Command/Query_Handler->_Service]".Underline()}\n");
            #endregion

        }

    }

}