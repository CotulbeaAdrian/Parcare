using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Application.Repository.Interfaces
{
    public interface IParkingRepository
    {
        SqlConnection conn { get; set; }

        void ParkCar(string carNumber, DateTime time);

        bool isCarParked(string carNumber);

        int ParkedCarsNumber();

        bool isPaymentReceived(string carNumber);

        void PayParking(string carNumber);

        void ExitParking(string carNumber);

        bool CarLeftParking(string carNumber);
    }
}
