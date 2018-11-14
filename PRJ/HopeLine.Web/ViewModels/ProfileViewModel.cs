using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    /// <summary>
    /// This class holds profile of user like name, gender, languages, etc
    /// </summary>
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel()
        {
            ProfileLanguages = new List<ProfileLanguageViewModel>();
        }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]

        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public ICollection<ProfileLanguageViewModel> ProfileLanguages { get; set; }

    }
}
