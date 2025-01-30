using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using pr.Models;

namespace pr.Data
{
    public class UserAccessor
    {
        private readonly string _connectionString = "Your_Connection_String_Here";

        // SIGNUP USER
        public bool RegisterUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (Name, Email, Password, Role) VALUES (@Name, @Email, @Password, @Role)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password); // Store hashed password in production
                cmd.Parameters.AddWithValue("@Role", user.Role);
                
                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // LOGIN USER
        public User LoginUser(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Email, Role FROM Users WHERE Email = @Email AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        Role = reader.GetString(3)
                    };
                }
                return null;
            }
        }
    }
}
