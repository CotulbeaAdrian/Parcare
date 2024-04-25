using Parking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Application.Services.Interfaces;

namespace Parking.Application.Services;

public class BankAccountService : IBankAccountService
{
    public List<BankAccountModel> Accounts { get; } = new List<BankAccountModel>();

    public void Add(string name, List<string> carNumber, float balance)
    {
        Accounts.Add(new BankAccountModel { Name = name, CarNumber = carNumber, Balance = balance });
    }

    public void Remove(string carNumber)
    {
        var accountsWithCarNumber = Accounts.FirstOrDefault(account => account.CarNumber.Contains(carNumber));

        if (accountsWithCarNumber != null)
        {
            Accounts.Remove(accountsWithCarNumber);
        }
    }

    public bool IsPaymentPossible(double amount, string carNumber)
    {
        var accountsWithCarNumber = Accounts.FirstOrDefault(account => account.CarNumber.Contains(carNumber));

        if (accountsWithCarNumber != null)
        {
            return (accountsWithCarNumber.Balance - amount) > 0;
        }

        return false;
    }
}
