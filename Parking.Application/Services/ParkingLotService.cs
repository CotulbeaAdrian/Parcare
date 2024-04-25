using Parking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Application.Services.Interfaces;

namespace Parking.Application.Services;

public class ParkingLotService : IParkingLotService
{
    private List<ParkingLotModel> _parkedCarsList;
    private int _maxSlots;
    private float _price;
    private BankAccountService _bank;

    public ParkingLotService(int totalSlots, float price, BankAccountService bankAccounts)
    {
        _parkedCarsList = new List<ParkingLotModel>();
        _maxSlots = totalSlots;
        _price = price;
        _bank = bankAccounts;
    }

    public bool IsParkingPossible()
    {
        return (_maxSlots - _parkedCarsList.Count) > 0;
    }

    public void ParkCar(string carNumber, DateTime time)
    {
        if (!IsParkingPossible())
        {
            Console.WriteLine($"There are no empty slots at the time. Please come back later!");
            return;
        }

        _parkedCarsList.Add(new ParkingLotModel
        {
            CarNumber = carNumber,
            EntryTime = time,
            PaymentReceived = false,
            TriedPayment = false,
            LeftParking = false
        });

        Console.WriteLine("Car is now parked.");
    }

    public bool IsCarParked(string carNumber)
    {
        ParkingLotModel? parkedCar = _parkedCarsList.Find(car => car.CarNumber == carNumber);
        return parkedCar != null;
    }

    public bool IsPaymentReceived(string carNumber)
    {
        ParkingLotModel? parkedCar = _parkedCarsList.Find(car => car.CarNumber == carNumber);

        if (parkedCar != null)
            return parkedCar.PaymentReceived;
        else
        {
            Console.WriteLine("There is no car with that number here.");
            return false;
        }
    }

    public void PayForParking(string carNumber)
    {
        ParkingLotModel? parkedCar = _parkedCarsList.Find(car => car.CarNumber == carNumber);

        if(parkedCar == null)
        {
            Console.WriteLine("No car found with that number.");
            return;
        }
    
        var duration = DateTime.Now - parkedCar.EntryTime;

        if (duration.TotalHours <= 1)
        {
            Console.WriteLine("Parking for an hour or less is free. Have a nice day!");
            parkedCar.PaymentReceived = true;
            return;
        }

        // -1 for the first hour which is free
        var paymentAmount = (duration.TotalHours) * _price;
        var accountWithCarNumber = _bank.accounts.FirstOrDefault(account => account.CarNumber.Contains(carNumber));

        if (accountWithCarNumber == null)
        {
            Console.WriteLine($"No account found with car number {carNumber}");
            return;
        }

        if(_bank.IsPaymentPossible(paymentAmount, carNumber))
        {
            accountWithCarNumber.Balance = (float)(accountWithCarNumber.Balance - paymentAmount);
            parkedCar.PaymentReceived = true;
            Console.WriteLine("Payment successful");
            return;
        }
        parkedCar.TriedPayment = true;
    }

    public void ExitParking(string carNumber)
    {
        ParkingLotModel? parkedCar = _parkedCarsList.Find(car => car.CarNumber == carNumber);

        if (parkedCar == null)
        {
            Console.WriteLine("No car found with that number.");
            return;
        }

        if(parkedCar.TriedPayment == false)
        {
            PayForParking(carNumber);
        }

        if(parkedCar.PaymentReceived == false)
        {
            Console.WriteLine("Payment is required first before exiting the parking lot.");
            return;
        }

        Console.WriteLine("Have a nice day!");
        parkedCar.LeftParking = true;
        _maxSlots++;
    }

    public bool CarLeftParking(string carNumber)
    {
        ParkingLotModel? parkedCar = _parkedCarsList.Find(car => car.CarNumber == carNumber);

        if (parkedCar != null)
            return parkedCar.LeftParking;
        else
        {
            Console.WriteLine("There is no car with that number here.");
            return false;
        }
    }
}
