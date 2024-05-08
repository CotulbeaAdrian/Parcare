using Microsoft.Data.SqlClient;
using Parking.Application.Context.Interfaces;
using Parking.Application.Repository.Interfaces;

namespace Parking.Application.Repository
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private SqlConnection _conn;

        public BankAccountRepository(IContext context)
        {
            _conn = context.Conn;
        }

        public void Add(string name,float balance)
        {
            var queryString = $"insert into UsersAccount (Name, Balance) Values ('{name}', '{balance}'";
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

        public void Remove(string carNumber)
        {
            var queryString = $"DELETE ua FROM UsersAccount ua JOIN UsersCar uc ON ua.userID = uc.userID WHERE uc.carNumber = '{carNumber}'";
            var delete = new SqlCommand(queryString, _conn);

            int rowsAffected = delete.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Record updated successfully.");
            }
            else
            {
                Console.WriteLine("No records were updated.");
            }
        }

        public double getBalance(string carNumber)
        {
            var queryString = $"SELECT ua.Balance FROM UsersCar uc INNER JOIN UsersAccount ua ON uc.userID = ua.userID WHERE uc.carNumber = '{carNumber}'";
            var result = new SqlCommand(queryString, _conn);
            var scalar = result.ExecuteScalar();
            return Convert.ToDouble(scalar);
        }

        public void Pay(double amount, string carNumber)
        {
            var queryString = $"UPDATE UsersAccount SET Balance = Balance - '{amount}' WHERE userID = (SELECT userID FROM UsersCar WHERE carNumber = '{carNumber}')";
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
    }
}
