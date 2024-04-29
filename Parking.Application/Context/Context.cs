using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Context
{
    public class Context
    {
        private SqlConnection _conn;
        private string _connectionString = "Server=DESKTOP-PN8PAKH;Database=test;User ID=sa;Password=passwd;Trusted_Connection=True;Trust Server Certificate=true;";

        public Context()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "passwd";
            builder.InitialCatalog = "test";
            _conn = new SqlConnection(_connectionString);
            _conn.Open();
            Console.WriteLine("Connection established!");
            _conn.Close();
        }
    }
}
