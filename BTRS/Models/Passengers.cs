using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BTRS.Models
{
    [Index(nameof(Passengers.username), IsUnique = true)]
    [Index(nameof(Passengers.email), IsUnique = true)]
    public class Passengers
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "*")]
        public string name { get; set; }
        [Required(ErrorMessage = "*")]

        public string email { get; set; }

        public string gender { get; set; }
        [Required(ErrorMessage = "*")]
        public string username { get; set; }
        [Required(ErrorMessage = "*")]
        public string password { get; set; }
        [Required(ErrorMessage = "*")]
        public int p_number { get; set; }
        public ICollection<PassengerBusTrip> BusTrip { get; set; }
    }
}
