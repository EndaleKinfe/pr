using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using pr.Models;

namespace pr.Data
{


    public class DeliveryManAccessor
    {
        private readonly Database _dbHelper;

        public DeliveryManAccessor()
        {
            _dbHelper = new Database();
        }

        // CREATE - Add a new Delivery Man
        public void AddDeliveryMan(DeliveryMan deliveryMan)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "INSERT INTO DeliveryMan (FirstName, LastName, PhoneNumber, VehicleType, LicenseNumber, IsAvailable) " +
                               "VALUES (@FirstName, @LastName, @PhoneNumber, @VehicleType, @LicenseNumber, @IsAvailable)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@FirstName", deliveryMan.FirstName);
                cmd.Parameters.AddWithValue("@LastName", deliveryMan.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", deliveryMan.PhoneNumber);
                cmd.Parameters.AddWithValue("@VehicleType", deliveryMan.VehicleType);
                cmd.Parameters.AddWithValue("@LicenseNumber", deliveryMan.LicenseNumber);
                cmd.Parameters.AddWithValue("@IsAvailable", deliveryMan.IsAvailable);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // READ - Get All DeliveryMen
        public List<DeliveryMan> GetAllDeliveryMen()
        {
            List<DeliveryMan> deliveryMen = new List<DeliveryMan>();

            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM DeliveryMan";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    deliveryMen.Add(new DeliveryMan
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        VehicleType = reader["VehicleType"].ToString(),
                        LicenseNumber = reader["LicenseNumber"].ToString(),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    });
                }
            }
            return deliveryMen;
        }

        // READ - Get a DeliveryMan by ID
        public DeliveryMan GetDeliveryManById(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM DeliveryMan WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new DeliveryMan
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        VehicleType = reader["VehicleType"].ToString(),
                        LicenseNumber = reader["LicenseNumber"].ToString(),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    };
                }
            }
            return null;
        }

        // UPDATE - Update DeliveryMan details
        public void UpdateDeliveryMan(DeliveryMan deliveryMan)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "UPDATE DeliveryMan SET FirstName=@FirstName, LastName=@LasttName, PhoneNumber=@PhoneNumber, " +
                               "VehicleType=@VehicleType, LicenseNumber=@LicenseNumber, IsAvailable=@IsAvailable " +
                               "WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", deliveryMan.Id);
                cmd.Parameters.AddWithValue("@FirstName", deliveryMan.FirstName);
                cmd.Parameters.AddWithValue("@LastName", deliveryMan.LastName);
                cmd.Parameters.AddWithValue("@PhoneNumber", deliveryMan.PhoneNumber);
                cmd.Parameters.AddWithValue("@VehicleType", deliveryMan.VehicleType);
                cmd.Parameters.AddWithValue("@LicenseNumber", deliveryMan.LicenseNumber);
                cmd.Parameters.AddWithValue("@IsAvailable", deliveryMan.IsAvailable);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE - Remove a DeliveryMan
        public void DeleteDeliveryMan(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "DELETE FROM DeliveryMan WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}
