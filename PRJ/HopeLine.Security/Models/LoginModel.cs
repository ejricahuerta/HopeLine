using System.ComponentModel.DataAnnotations;

namespace HopeLine.Security.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            IsGuest = false;
        }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsGuest { get; set; }
    }
}

