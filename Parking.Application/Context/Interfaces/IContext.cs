using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Context.Interfaces
{
    public interface IContext
    {
        SqlConnection Conn { get; set; }

        string ConnectionString { get; set; }

        void CloseConnection();
    }
}
