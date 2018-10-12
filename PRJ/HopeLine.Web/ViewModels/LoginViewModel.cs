using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(40)]
        [MinLength(5)]
        public string Username { get; set; }
        
        [MinLength(5)]
        [MaxLength(25)]
        [Required]
        public string  Password { get; set; }
    }
}