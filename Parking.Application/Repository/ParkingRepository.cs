using Microsoft.Data.SqlClient;
using Parking.Application.Context.Interfaces;
using Parking.Application.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Repository
{
    public class ParkingRepository : IParkingRepository
    {
        public SqlConnection conn { get; set; }

        public ParkingRepository(IContext context)
        {
            conn = context.Conn;
        }
        public void ParkCar(string carNumber, DateTime time)
        {
            var queryString = $"select pl.PaymentReceived from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}'";
            var result = new SqlCommand(queryString, conn);
            using (var reader = result.ExecuteReader())
            {
                while (reader.Read())
                {
                    var name = reader.GetString(1);
                    Console.WriteLine(name);
                }
            }
        }

        public bool isCarParked(string carNumber)
        {
            //
            return true;
        }

        public int ParkedCarsNumber()
        {
            var queryString = "select count(carID) from ParkingLog where ExitTime is NULL";
            var result = new SqlCommand(queryString, conn);
            using (var reader = result.ExecuteReader())
            {
                while (reader.Read())
                {
                    var count = reader.GetString(1);
                    Console.WriteLine(count);
                }
            }
            return 1;
        }

        public bool isPaymentReceived(string carNumber)
        {
            // TO BE IMPLEMENTED
            return true;
        }

        public void PayParking(string carNumber)
        {
            // TO BE IMPLEMENTED
        }

        public void ExitParking(string carNumber)
        {
            // TO BE IMPLEMENTED
        }

        public bool CarLeftParking(string carNumber)
        {
            // TO BE IMPLEMENTED
            return true;
        }
    }
}
