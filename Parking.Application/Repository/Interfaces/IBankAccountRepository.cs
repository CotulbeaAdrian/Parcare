using Microsoft.Data.SqlClient;
using Parking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Repository.Interfaces
{
    public interface IBankAccountRepository
    {
        void Add(string Name, float balance);

        void Remove(string carNumber);

        double getBalance(string carNumber);

        void Pay(double amount, string carNumber);
    }
}
