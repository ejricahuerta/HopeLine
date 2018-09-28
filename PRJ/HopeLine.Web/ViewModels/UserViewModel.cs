using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Web.ViewModels
{
    public interface IUserViewModel
    {
        string Id { get; set; }
    }

    /// <summary>
    /// DTO fo users. this does not contain any logic at all.
    /// it is a Dummy data transfer object
    /// </summary>
    public class UserViewModel : IUserViewModel
    {
        [Required]
        public string Id { get; set; }
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [DataType(DataType.PhoneNumber)]
        [StringLength(11)]
        public string Phone { get; set; }
        public ICollection<string> Languages { get; set; }
        public string AccountType { get; set; }

        public UserViewModel()
        {
            Languages = new List<string>();
        }
    }

}
