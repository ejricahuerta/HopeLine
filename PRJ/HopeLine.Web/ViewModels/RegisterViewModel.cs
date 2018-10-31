using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [MinLength(15)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Invalid First Name")]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Invalid Last Name")]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public string Phone { get; set; }


        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Compare("Password")]
        [MinLength(6)]
        [MaxLength(20)]
        public string RetypePassword { get; set; }
    }
}
