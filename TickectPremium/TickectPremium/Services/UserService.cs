using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;
using TickectPremium.Models;
using TickectPremium.Resources;

namespace TickectPremium.Services
{
    public class UserService
    {
        private string connectionString; // Cadena de conexión a la base de datos

        public UserService()
        {
            this.connectionString = Constant.stringDeConexion;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Usuarios WHERE USUARIO = @Username AND CONTRASENA = @Password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User usuario = new User
                            {
                                IDUser = Convert.ToInt32(reader["ID_USUARIO"]),
                                Username = reader["USUARIO"].ToString(),
                                Password = reader["CONTRASENA"].ToString(),
                                Email = reader["EMAIL"].ToString(),
                                Name = reader["NOMBRE"].ToString(),
                                LastName = reader["APELLIDO"].ToString(),
                                Phone = reader["TELEFONO"].ToString()
                            };

                            return usuario;
                        }
                    }
                }
            }

            return null;
        }


        public bool Login(LoginModel userLogin)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Usuarios WHERE USUARIO = @Username AND CONTRASENA = @Password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", userLogin.Username);
                    command.Parameters.AddWithValue("@Password", userLogin.Password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        public bool SigninUser(UserDTO user)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Usuarios (USUARIO, CONTRASENA, EMAIL, NOMBRE, APELLIDO, TELEFONO) " +
                               "VALUES (@Usuario, @Contrasena, @Email, @Nombre, @Apellido, @Telefono)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", user.Username);
                    command.Parameters.AddWithValue("@Contrasena", user.Password);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Nombre", user.Name);
                    command.Parameters.AddWithValue("@Apellido", user.LastName);
                    command.Parameters.AddWithValue("@Telefono", user.Phone);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
    }
}
