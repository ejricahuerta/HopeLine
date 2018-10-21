using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [MinLength(20)]
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


        [Required(ErrorMessage = "Invalid Password")]
        [MinLength(6)]
        [StringLength(20)]
        public string Password { get; set; }

        [Compare("Password")]
        [Required(ErrorMessage = "Invalid Password")]
        [MinLength(6)]
        [StringLength(20)]
        public string RetypePassword { get; set; }
    }
}