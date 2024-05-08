using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parking.Application.Entities
{
    public class Car
    {
        [Key] // Marks the property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configures it to auto-increment
        public int CarID { get; set; }

        public string CarNumber { get; set; }

        public int UserID { get; set; }

        public List<ParkingLot> Lots { get; set; }

        public User User { get; set; }
    }
}
