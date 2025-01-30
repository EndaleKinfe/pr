using System;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace pr.Data
{


    public class Database
    {
        private readonly string _connectionString;

        public Database()
        {
            _connectionString = @"Data Source=ENDALE-PC;Initial Catalog=enbla;Integrated Security=True";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
