using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(40)]
        public string Username { get; set; }
        
        [MinLength(6)]
        [MaxLength(25)]
        [Required]
        public string  Password { get; set; }
    }
}