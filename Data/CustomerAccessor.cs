using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using pr.Models;

namespace pr.Data
{


    public class CustomerAccessor
    {
        private readonly Database _dbHelper;

        public CustomerAccessor()
        {
            _dbHelper = new Database();
        }

        // CREATE - Add a new Customer
        public void AddCustomer(Customer customer)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber, Address) " +
                               "VALUES (@FirstName,@LastName, @Email, @PhoneNumber, @Address)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customer.Address);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // READ - Get All Customers
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM Customer";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString()
                    });
                }
            }
            return customers;
        }

        // READ - Get a Customer by ID
        public Customer GetCustomerById(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "SELECT * FROM Customer WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Customer
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Address = reader["Address"].ToString()
                    };
                }
            }
            return null;
        }

        // UPDATE - Update Customer details
        public void UpdateCustomer(Customer customer)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "UPDATE Customer SET FirstName=@FirstName, LastName=@LasttName, Email=@Email, PhoneNumber=@PhoneNumber, " +
                               "Address=@Address WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", customer.Id);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", customer.Address);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE - Remove a Customer
        public void DeleteCustomer(int id)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = "DELETE FROM Customer WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}

