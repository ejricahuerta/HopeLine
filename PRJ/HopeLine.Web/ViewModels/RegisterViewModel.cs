using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MinLength(20)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public string Phone { get; set; }


        [MinLength(6)]
        [StringLength(20)]
        public string Password { get; set; }
    }
}