
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Full_name { get; set; }
        public ICollection<BusTrip> bus_trip { get; set; }
    }
}
