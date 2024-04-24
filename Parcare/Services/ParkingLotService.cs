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

        public ParkingLotService(int totalSlots)
        {
            _parkedCarsList = new List<ParkingLotModel>();
            _maxSlots = totalSlots;
        }

        public bool IsParkingPossible()
        {
            return (_maxSlots - _parkedCarsList.Count) > 0;
        }
        public void ParkCar(string carNumber)
        {
            if (IsParkingPossible())
            {
                _parkedCarsList.Add(new ParkingLotModel
                {
                    CarNumber = carNumber,
                    EntryTime = DateTime.Now,
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
            ParkingLotModel parkedCar = _parkedCarsList.Find(car => car.CarNumber == carNumber);
            return (parkedCar != null);
        }

    }
}
