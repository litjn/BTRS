using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class PassengerBusTrip
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("PassengerID")]
        public Passengers passenger { get; set; }
        [ForeignKey("Bus_TripID")]
        public BusTrip bus_trip { get; set; }
    }
}
