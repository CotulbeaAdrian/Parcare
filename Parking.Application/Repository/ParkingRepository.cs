using Microsoft.Data.SqlClient;
using Parking.Application.Context.Interfaces;
using Parking.Application.Models;
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
        public ParkingLotModel GetByCarNumber(string carNumber)
        {
            var queryString = $"select * from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}'";
            var result = new SqlCommand(queryString, conn);
            using (var reader = result.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new ParkingLotModel { CarNumber = reader.GetString(1), EntryTime = reader.GetDateTime(5), LeftParking = (reader.GetByte(8) == 1), PaymentReceived = (reader.GetByte(6) == 1), TriedPayment = (reader.GetByte(7) == 1) };
                }
                return null;
            }
        }

        public void ParkCar(string carNumber, DateTime time)
        {
            var queryString = $"insert into ParkingLog (carID, EntryTime) Values ({carNumber}, {time}";
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
            var queryString = $"select * from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var result = new SqlCommand(queryString, conn);
            using (var reader = result.ExecuteReader())
            {
                if (reader.Read())
                {
                    return true;
                }
                return false;
            }
        }

        public int ParkedCarsNumber()
        {
            var queryString = "select count(carID) from ParkingLog where ExitTime is NULL";
            var result = new SqlCommand(queryString, conn);
            using (var reader = result.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
                return 0;
            }
        }

        public bool isPaymentReceived(string carNumber)
        {
            var queryString = $"select pl.PaymentReceived from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var result = new SqlCommand(queryString, conn);
            using (var reader = result.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetByte(0) == 1;
                }
                return false;
            }
        }

        public bool isPaymentTried(string carNumber)
        {
            var queryString = $"select pl.TriedPayment from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var result = new SqlCommand(queryString, conn);
            using (var reader = result.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetByte(0) == 1;
                }
                return false;
            }
        }

        public void PaidForParking(string carNumber)
        {
            var queryString = $"update ParkingLog set PaymentReceived = 1, TriedPayment = 1 from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var update = new SqlCommand(queryString, conn);
            
            int rowsAffected = update.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Record updated successfully.");
            }
            else
            {
                Console.WriteLine("No records were updated.");
            }
        }

        public void TriedPayment(string carNumber)
        {
            var queryString = $"update ParkingLog set TriedPayment = 1 from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var update = new SqlCommand(queryString, conn);

            int rowsAffected = update.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Record updated successfully.");
            }
            else
            {
                Console.WriteLine("No records were updated.");
            }
        }

        public void ExitParking(string carNumber, DateTime time)
        {
            var queryString = $"update ParkingLog set PaymentReceived = 1, ExitTime = {time} from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var update = new SqlCommand(queryString, conn);

            int rowsAffected = update.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Record updated successfully.");
            }
            else
            {
                Console.WriteLine("No records were updated.");
            }
        }

        public bool CarLeftParking(string carNumber)
        {
            var queryString = $"select * from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var result = new SqlCommand(queryString, conn);
            using (var reader = result.ExecuteReader())
            {
                if (reader.Read())
                {
                    return false;
                }
                return true;
            }
        }
    }
}
