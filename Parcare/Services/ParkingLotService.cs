using Parking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Services
{
    public class ParkingLotService
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
            if (IsParkingPossible())
            {
                _parkedCarsList.Add(new ParkingLotModel
                {
                    CarNumber = carNumber,
                    EntryTime = time,
                    PaymentReceived = false
                });
                Console.WriteLine("Car is now parked.");
            }
            else
            {
                Console.WriteLine($"There are no empty slots at the time. Please come back later!");
            }
        }
        public bool IsCarParked(string carNumber)
        {
            ParkingLotModel? parkedCar = _parkedCarsList.Find(car => car.CarNumber == carNumber);
            return (parkedCar != null);
        }

        public bool IsPaymentReceived(string carNumber)
        {
            ParkingLotModel? parkedCar = _parkedCarsList.Find(car => car.CarNumber == carNumber);
            if (parkedCar != null )
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
            if(parkedCar != null)
            {
                
                var duration = DateTime.Now - parkedCar.EntryTime;

                Console.WriteLine(parkedCar.EntryTime);
                if (duration.TotalHours <= 1)
                {
                    Console.WriteLine("Parking for an hour or less is free. Have a nice day!");
                    parkedCar.PaymentReceived = true;
                }
                else
                {
                    // -1 for the first hour which is free
                    var paymentAmount = (duration.TotalHours) * _price;
                    var accountWithCarNumber = _bank.accounts.FirstOrDefault(account => account.CarNumber.Contains(carNumber));
                    if(accountWithCarNumber != null)
                    {
                        if(_bank.CanPay(paymentAmount, carNumber))
                        {
                            accountWithCarNumber.Balance = (float)(accountWithCarNumber.Balance - paymentAmount);
                            parkedCar.PaymentReceived = true;
                            Console.WriteLine("Payment successful");
                        }
                        else
                        {
                            Console.WriteLine("Payment failed. Balance too low.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No account found with car number {carNumber}");
                    }
                }
            }
        }
    }
}
