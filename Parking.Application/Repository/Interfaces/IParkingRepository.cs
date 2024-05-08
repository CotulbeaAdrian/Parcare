using Microsoft.Data.SqlClient;
using Parking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Repository.Interfaces
{
    public interface IParkingRepository
    {
        DateTime getEntryTime(string carNumber);

        void ParkCar(string carNumber, DateTime time);

        bool isCarParked(string carNumber);

        int getCarID(string carNumber);

        int ParkedCarsNumber();

        bool isPaymentReceived(string carNumber);

        bool isPaymentTried(string carNumber);

        void TriedPayment(string carNumber);

        void PaidForParking(string carNumber);

        void ExitParking(string carNumber, DateTime time);

        bool CarLeftParking(string carNumber);
    }
}
