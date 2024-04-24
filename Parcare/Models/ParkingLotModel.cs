using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Models
{
    public class ParkingLotModel
    {
        public required string CarNumber { get; set; }
        public required DateTime EntryTime { get; set; }
        public required bool PaymentReceived { get; set; }
    }
}
