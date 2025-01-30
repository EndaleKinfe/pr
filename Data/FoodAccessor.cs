using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using pr.Models;

namespace pr.Data
{


    public class FoodAccessor
    {
        private readonly Database _dbHelper;

        public FoodAccessor()
        {
            _dbHelper = new Database();
        }

        // CREATE
        public void AddFood(Food food)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "INSERT INTO Food (Name, Description, Price, Category, ImageUrl, RestaurantId) " +
                               "VALUES (@Name, @Description, @Price, @Category, @ImageUrl, @RestaurantId)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", food.Name);
                cmd.Parameters.AddWithValue("@Description", food.Description);
                cmd.Parameters.AddWithValue("@Price", food.Price);
                cmd.Parameters.AddWithValue("@Category", food.Category);
                cmd.Parameters.AddWithValue("@ImageUrl", food.ImageUrl);
                cmd.Parameters.AddWithValue("@RestaurantId", food.RestaurantId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // READ (Get All Foods)
        public List<Food> GetAllFoods()
        {
            List<Food> foods = new List<Food>();

            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM Food";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    foods.Add(new Food
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Category = reader["Category"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        RestaurantId = Convert.ToInt32(reader["RestaurantId"])
                    });
                }
            }
            return foods;
        }

        // READ (Get by ID)
        public Food GetFoodById(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM Food WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Food
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Category = reader["Category"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        RestaurantId = Convert.ToInt32(reader["RestaurantId"])
                    };
                }
            }
            return null;
        }

        // UPDATE
        public void UpdateFood(Food food)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "UPDATE Food SET Name=@Name, Description=@Description, Price=@Price, " +
                               "Category=@Category, ImageUrl=@ImageUrl, RestaurantId=@RestaurantId WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", food.Id);
                cmd.Parameters.AddWithValue("@Name", food.Name);
                cmd.Parameters.AddWithValue("@Description", food.Description);
                cmd.Parameters.AddWithValue("@Price", food.Price);
                cmd.Parameters.AddWithValue("@Category", food.Category);
                cmd.Parameters.AddWithValue("@ImageUrl", food.ImageUrl);
                cmd.Parameters.AddWithValue("@RestaurantId", food.RestaurantId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE
        public void DeleteFood(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "DELETE FROM Food WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}
