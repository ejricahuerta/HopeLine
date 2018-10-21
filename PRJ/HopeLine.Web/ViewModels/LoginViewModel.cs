using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(40)]
        [MinLength(5)]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        public string Password { get; set; }
    }
}