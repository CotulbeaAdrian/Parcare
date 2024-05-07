using Microsoft.Data.SqlClient;
using Parking.Application.Context.Interfaces;
using Parking.Application.Models;
using Parking.Application.Repository.Interfaces;
using Parking.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        public SqlConnection conn { get; set; }

        public BankAccountRepository(IContext context)
        {
            conn = context.Conn;
        }

        public void Add(BankAccountModel model)
        {
            // TO BE IMPLEMENTED
        }

        public void Remove(BankAccountModel model)
        {
            // TO BE IMPLEMENTED
        }

        public bool IsPaymentPossible(double amount, string carNumber)
        {
            // TO BE IMPLEMENTED
            return true;
        }
    }
}
