using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels {
    public class LoginViewModel {
        [Required (ErrorMessage = "Enter a valid email address")]
        [MaxLength (40)]
        [MinLength (5)]
        [DataType (DataType.EmailAddress)]
        public string Username { get; set; }

        [Required (ErrorMessage = "Password is Required")]
        [StringLength (25, MinimumLength = 8, ErrorMessage = "Password must be between {1} and {0} characters")]
        [DataType (DataType.Password)]
        public string Password { get; set; }
    }
}