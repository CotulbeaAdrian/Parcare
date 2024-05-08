using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Parking.Application.Entities
{
    public class User
    {
        [Key] // Marks the property as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configures it to auto-increment
        public int UserID { get; set; }
        public string name { get; set; }
        public double Balance { get; set; }
        public List<Car> Cars { get; set; }
    }
}
