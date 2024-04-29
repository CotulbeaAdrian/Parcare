using Parking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Services.Interfaces;
public interface IBankAccountService
{
    List<BankAccountModel> Accounts { get; }
    void Add(string name, List<string> carNumber, float balance);
    void Remove(string carNumber);
    bool IsPaymentPossible(double amount, string carNumber);
}