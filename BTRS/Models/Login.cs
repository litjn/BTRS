using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please fill out the missing information !")]
        public string username { set; get; }
        [Required(ErrorMessage = "Please fill out the missing information !")]
        public string password { set; get; }
    }
}
