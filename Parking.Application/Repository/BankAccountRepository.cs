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
            GetByCarNumber("car2");
        }
        public ParkingLotModel GetByCarNumber(string carNumber)
        {
            var queryString = $"select * from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}'";
            var result = new SqlCommand(queryString, conn);
            using (var reader = result.ExecuteReader()) {
                while (reader.Read())
                {
                    var name = reader.GetString(1);
                    Console.WriteLine(name);
                }
            }

            return new ParkingLotModel { CarNumber = carNumber , EntryTime = DateTime.Now, LeftParking = false, PaymentReceived = false, TriedPayment = false};
            
            
        }

        public IEnumerable<BankAccountModel> GetAll()
        {
            // TO BE IMPLEMENTED
            return null;
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
