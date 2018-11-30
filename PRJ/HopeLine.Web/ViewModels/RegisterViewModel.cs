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

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage ="Password must contain atleast an uppercase, a symbol, and a number.")]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Password does not Match.")]
        
        [MinLength(6)]
        [MaxLength(20)]
        public string RetypePassword { get; set; }

        [Required(ErrorMessage = "Invalid SIN")]
        [MinLength(9)]
        public string SIN { get; set; }
    }
}
