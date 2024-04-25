using Parking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Services;

public class BankAccountService
{
    public List<BankAccountModel> accounts = new List<BankAccountModel>();

    public void Add(string name, List<string> carNumber, float balance)
    {
        accounts.Add(new BankAccountModel { Name = name, CarNumber = carNumber, Balance = balance });
    }

    public void Remove(string carNumber)
    {
        var accountsWithCarNumber = accounts.FirstOrDefault(account => account.CarNumber.Contains(carNumber));

        if (accountsWithCarNumber != null)
        {
            accounts.Remove(accountsWithCarNumber);
        }
    }

    public bool IsPaymentPossible(double amount, string carNumber)
    {
        var accountsWithCarNumber = accounts.FirstOrDefault(account => account.CarNumber.Contains(carNumber));

        if (accountsWithCarNumber != null)
        {
            return (accountsWithCarNumber.Balance - amount) > 0;
        }

        return false;
    }
}
