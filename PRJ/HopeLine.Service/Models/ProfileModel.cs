using HopeLine.Service.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HopeLine.Service.Models
{
    public class ProfileModel : BaseModel
    {
        public ProfileModel()
        {
            ProfileLanguages = new List<ProfileLanguageModel>();
        }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]

        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public ICollection<ProfileLanguage> ProfileLanguages { get; set; }
    }
}
