using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Application.Services;

namespace Parking.Application.Services.Interfaces;

public interface IParkingLotService
{
    bool IsParkingPossible(string carNumber);
    void ParkCar(string carNumber, DateTime time);
    bool IsCarParked(string carNumber);
    bool IsPaymentReceived(string carNumber);
    void PayForParking(string carNumber);
    void ExitParking(string carNumber);
    bool CarLeftParking(string carNumber);
}
