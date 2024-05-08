using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parking.Application.Entities
{
    public class ParkingLot
    {
        [Key] // Marks the property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configures it to auto-increment
        public int ID { get; set; }
        public int CarID { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public bool PaymentReceived { get; set; }
        public bool TriedPayment { get; set; }
        public bool LeftParking { get; set; }
        public Car Car { get; set; }

        public ParkingLot()
        {
            PaymentReceived = false;
            TriedPayment = false;
            LeftParking = false;
            ExitTime = null;
        }
    }
}
