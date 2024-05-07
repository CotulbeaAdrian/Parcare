﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Parking.Application.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Context
{
    public class Context : IContext
    {
        public SqlConnection Conn { get; set; }
        string IContext.ConnectionString { get; set; }

        public Context(string connectionString)
        {
            ConnectToDB(connectionString);
        }

        private void ConnectToDB(string connectionString)
        {
            Conn = new SqlConnection(connectionString);
            Conn.Open();
            Console.WriteLine("Connection established!");
        }

        public void CloseConnection() { Conn.Close();
            Console.WriteLine("Connection closed!");
        }
    }
}
