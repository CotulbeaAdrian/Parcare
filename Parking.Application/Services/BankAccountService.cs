using Parking.Application.Services.Interfaces;
using Parking.Application.Repository.Interfaces;

namespace Parking.Application.Services;

public class BankAccountService : IBankAccountService
{
    private IBankAccountRepository _bankAccountRepository;

    public BankAccountService(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }

    public void Add(string name, List<string> carNumber, float balance)
    {
        _bankAccountRepository.Add(name, balance);
    }

    public void Remove(string carNumber)
    {
        _bankAccountRepository.Remove(carNumber);
    }

    public bool IsPaymentPossible(double amount, string carNumber)
    {
        return (_bankAccountRepository.getBalance(carNumber) - amount) > 0;
    }

    public void Pay(double amount, string carNumber)
    {
        _bankAccountRepository.Pay(amount, carNumber);
    }
}
