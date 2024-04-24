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
        private List<ParkingLotModel> _parkedCars;
        private int _maxSlots;
        private int _availableSlots;

        public ParkingLotService(int totalSlots)
        {
            this._parkedCars = new List<ParkingLotModel>();
            this._maxSlots = totalSlots;
            this._availableSlots = _maxSlots - _parkedCars.Count;
        }

        public bool IsParkingPossible()
        {
            return _availableSlots > 0;
        }
        public void ParkCar(string carNumber)
        {
            if (IsParkingPossible())
            {
                this._parkedCars.Add(new ParkingLotModel
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
            ParkingLotModel parkedCar = _parkedCars.Find(car => car.CarNumber == carNumber);
            return (parkedCar != null);
        }

    }
}
