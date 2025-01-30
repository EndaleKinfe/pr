using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using pr.Models;

namespace pr.Data
{


    public class OrderAccessor
    {
        private readonly Database _dbHelper;

        public OrderAccessor()
        {
            _dbHelper = new Database();
        }

        // CREATE - Add a new Order
        public void AddOrder(Order order)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "INSERT INTO [Order] (CustomerId, RestaurantId, DeliveryManId, OrderDate, Status, TotalAmount, FoodId) " +
                               "VALUES (@CustomerId, @RestaurantId, @DeliveryManId, @OrderDate, @Status, @TotalAmount, @FoodId)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                cmd.Parameters.AddWithValue("@RestaurantId", order.RestaurantId);
                cmd.Parameters.AddWithValue("@DeliveryManId", (object)order.DeliveryManId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@Status", order.Status);
                cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                cmd.Parameters.AddWithValue("@FoodId", order.FoodId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // READ - Get All Orders
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM [Order]";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        RestaurantId = Convert.ToInt32(reader["RestaurantId"]),
                        DeliveryManId = reader["DeliveryManId"] != DBNull.Value ? Convert.ToInt32(reader["DeliveryManId"]) : 0,
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        Status = reader["Status"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        FoodId = reader["FoodId"].ToString()
                    });
                }
            }
            return orders;
        }

        // READ - Get an Order by ID
        public Order GetOrderById(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM [Order] WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Order
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CustomerId = Convert.ToInt32(reader["CustomerId"]),
                        RestaurantId = Convert.ToInt32(reader["RestaurantId"]),
                        DeliveryManId = reader["DeliveryManId"] != DBNull.Value ? Convert.ToInt32(reader["DeliveryManId"]) : 0,
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        Status = reader["Status"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        FoodId = reader["FoodId"].ToString()
                    };
                }
            }
            return null;
        }

        // UPDATE - Update Order details
        public void UpdateOrder(Order order)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "UPDATE [Order] SET CustomerId=@CustomerId, RestaurantId=@RestaurantId, DeliveryManId=@DeliveryManId, " +
                               "OrderDate=@OrderDate, Status=@Status, TotalAmount=@TotalAmount, FoodId=@FoodId WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", order.Id);
                cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
                cmd.Parameters.AddWithValue("@RestaurantId", order.RestaurantId);
                cmd.Parameters.AddWithValue("@DeliveryManId", (object)order.DeliveryManId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@Status", order.Status);
                cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                cmd.Parameters.AddWithValue("@FoodId", order.FoodId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE - Remove an Order
        public void DeleteOrder(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "DELETE FROM [Order] WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}
