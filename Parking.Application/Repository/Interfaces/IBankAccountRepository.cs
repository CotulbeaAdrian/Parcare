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
        SqlConnection conn { get; set; }

        void Add(BankAccountModel model);

        void Remove(BankAccountModel model);

        bool IsPaymentPossible(double amount, string carNumber);
    }
}
