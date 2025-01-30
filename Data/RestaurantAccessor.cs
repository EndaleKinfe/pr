using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using pr.Models;

namespace pr.Data
{


    public class RestaurantAccessor
    {
        private readonly Database _dbHelper;

        public RestaurantAccessor()
        {
            _dbHelper = new Database();
        }

        // CREATE - Add a new Restaurant
        public void AddRestaurant(Restaurant restaurant)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "INSERT INTO Restaurant (Name, Address, PhoneNumber, CuisineType) " +
                               "VALUES (@Name, @Address, @PhoneNumber, @CuisineType)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                cmd.Parameters.AddWithValue("@Address", restaurant.Address);
                cmd.Parameters.AddWithValue("@PhoneNumber", restaurant.PhoneNumber);
                cmd.Parameters.AddWithValue("@CuisineType", restaurant.CuisineType);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // READ - Get All Restaurants
        public List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM Restaurant";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    restaurants.Add(new Restaurant
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        CuisineType = reader["CuisineType"].ToString()
                    });
                }
            }
            return restaurants;
        }

        // READ - Get a Restaurant by ID
        public Restaurant GetRestaurantById(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM Restaurant WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Restaurant
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        CuisineType = reader["CuisineType"].ToString()
                    };
                }
            }
            return null;
        }

        // UPDATE - Update Restaurant details
        public void UpdateRestaurant(Restaurant restaurant)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "UPDATE Restaurant SET Name=@Name, Address=@Address, PhoneNumber=@PhoneNumber, " +
                               "CuisineType=@CuisineType WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", restaurant.Id);
                cmd.Parameters.AddWithValue("@Name", restaurant.Name);
                cmd.Parameters.AddWithValue("@Address", restaurant.Address);
                cmd.Parameters.AddWithValue("@PhoneNumber", restaurant.PhoneNumber);
                cmd.Parameters.AddWithValue("@CuisineType", restaurant.CuisineType);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE - Remove a Restaurant
        public void DeleteRestaurant(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "DELETE FROM Restaurant WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}
