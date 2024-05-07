using Parking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Application.Services.Interfaces;
using Parking.Application.Configuration;
using Microsoft.Extensions.Options;
using Parking.Application.Repository.Interfaces;

namespace Parking.Application.Services;

public class ParkingLotService : IParkingLotService
{
    private int _maxSlots;
    private float _price;
    private IBankAccountService _bank;
    private IParkingRepository _parkingRepository;

    public ParkingLotService(IBankAccountService bankAccounts, IOptions<ApplicationKeys> applicationKeys, IParkingRepository parkingRepository)
    {
        _maxSlots = applicationKeys.Value.TotalSlots;
        _price = applicationKeys.Value.Price;
        _bank = bankAccounts;
        _parkingRepository = parkingRepository;
    }

    public bool IsParkingPossible()
    {
        return (_maxSlots - _parkingRepository.ParkedCarsNumber()) > 0; 
    }

    public void ParkCar(string carNumber, DateTime time)
    {
        if (!IsParkingPossible())
        {
            Console.WriteLine($"There are no empty slots at the time.");
            return;
        }

        _parkingRepository.ParkCar(carNumber, time);
        Console.WriteLine("Car is now parked.");
    }

    public bool IsCarParked(string carNumber)
    {
        return _parkingRepository.isCarParked(carNumber);
    }

    public bool IsPaymentReceived(string carNumber)
    {
        return _parkingRepository.isPaymentReceived(carNumber);
    }

    public void PayForParking(string carNumber)
    {
        ParkingLotModel parkedCar = _parkingRepository.GetByCarNumber(carNumber);

        if(parkedCar == null)
        {
            Console.WriteLine("No car found with that number.");
            return;
        }
    
        var duration = DateTime.Now - parkedCar.EntryTime;

        if (duration.TotalHours <= 1)
        {
            Console.WriteLine("Parking for an hour or less is free. Have a nice day!");
            _parkingRepository.PaidForParking(carNumber);
            return;
        }

        // -1 for the first hour which is free
        var paymentAmount = (duration.TotalHours - 1) * _price;
        var accountWithCarNumber = _bank.Accounts.FirstOrDefault(account => account.CarNumber.Contains(carNumber));

        if (accountWithCarNumber == null)
        {
            Console.WriteLine($"No account found with car number {carNumber}");
            return;
        }

        if(_bank.IsPaymentPossible(paymentAmount, carNumber))
        {
            accountWithCarNumber.Balance = (float)(accountWithCarNumber.Balance - paymentAmount);
            _parkingRepository.PaidForParking(carNumber);
            Console.WriteLine("Payment successful");
            return;
        }
        Console.WriteLine("Payment unsuccessful");
        _parkingRepository.TriedPayment(carNumber);
    }

    public void ExitParking(string carNumber)
    {
        ParkingLotModel parkedCar = _parkingRepository.GetByCarNumber(carNumber);

        if (parkedCar == null)
        {
            Console.WriteLine("No car found with that number.");
            return;
        }

        if(!_parkingRepository.isPaymentTried(carNumber))
        {
            PayForParking(carNumber);
        }

        if(!_parkingRepository.isPaymentReceived(carNumber))
        {
            Console.WriteLine("Payment is required first before exiting the parking lot.");
            return;
        }

        Console.WriteLine("Have a nice day!");
        _parkingRepository.ExitParking(carNumber, DateTime.Now);
    }

    public bool CarLeftParking(string carNumber)
    {
        return _parkingRepository.CarLeftParking(carNumber);
    }
}
