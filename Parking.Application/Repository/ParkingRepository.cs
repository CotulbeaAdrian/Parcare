using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Parking.Application.Context;
using Parking.Application.Context.Interfaces;
using Parking.Application.Repository.Interfaces;

namespace Parking.Application.Repository
{
    public class ParkingRepository : IParkingRepository
    {
        private SqlConnection _conn;
        private readonly AppDBContext _dbContext;
        public ParkingRepository(IContext context, AppDBContext dbContext)
        {
            _conn = context.Conn;
            _dbContext = dbContext;
            var userList = _dbContext.Users.ToList();
        }
        public DateTime getEntryTime(string carNumber)
        {
            var queryString = $"select pl.EntryTime from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var result = new SqlCommand(queryString, _conn);
            var scalar = result.ExecuteScalar();
            return (DateTime)scalar;
        }

        public int getCarID(string carNumber) 
        {
            var queryString = $"select carID from UsersCar where carNumber = '{carNumber}'";
            var result = new SqlCommand(queryString, _conn);
            var scalar = result.ExecuteScalar();
            if(scalar != null)
            {
                return (int)scalar;
            }
            return 0;
        }

        public void ParkCar(string carNumber, DateTime time)
        {
            var carID = getCarID(carNumber);
            if (carID == 0)
                return;
            var queryString = $"insert into ParkingLog (carID, EntryTime, PaymentReceived, TriedPayment, LeftParking) Values ('{carID}', '{time}',0,0,0)";
            var insert = new SqlCommand(queryString, _conn);
            int rowsAffected = insert.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Record inserted successfully.");
            }
            else
            {
                Console.WriteLine("No records were inserted.");
            }
        }

        public bool isCarParked(string carNumber)
        {
            var queryString = $"select * from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var result = new SqlCommand(queryString, _conn);
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
            var result = new SqlCommand(queryString, _conn);
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
            var result = new SqlCommand(queryString, _conn);
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
            var result = new SqlCommand(queryString, _conn);
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
            var update = new SqlCommand(queryString, _conn);
            
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
            var update = new SqlCommand(queryString, _conn);

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
            var queryString = $"update ParkingLog set LeftParking = 1, ExitTime = '{time}' from UsersCar as uc join ParkingLog as pl on uc.carID = pl.carID where uc.carNumber = '{carNumber}' and pl.ExitTime is null";
            var update = new SqlCommand(queryString, _conn);

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
            var result = new SqlCommand(queryString, _conn);
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
