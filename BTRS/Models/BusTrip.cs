using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BTRS.Models
{
    public class BusTrip
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please fill out the missing information !")]
        public string trip_destination { get; set; }
        [Required(ErrorMessage = "Please fill out the missing information !")]
        public int bus_number { get; set; }
        [Required(ErrorMessage = "Please fill out the missing information !")]
        public DateTime date { get; set; }
        [ForeignKey("AdminID")]
        public Admin admin { get; set; }
        public ICollection<PassengerBusTrip> bus_trip { get; set; }
        public ICollection<Bus> buses { get; set; }
    }
}
