using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BTRS.Models
{
    public class Bus
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please fill out the missing information !")]
        public string captain_name { get; set; }
        [Required(ErrorMessage = "Please fill out the missing information !")]
        public int num_seat { get; set; }
        [ForeignKey("Bus_TripID")]
        public BusTrip bustrip { get; set; }
    }
}
