using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    /// <summary>
    /// This class holds profile of user like name, gender, languages, etc
    /// </summary>
    public class Profile : BaseEntity
    {
        public Profile()
        {
            ProfileLanguages = new List<ProfileLanguage>();
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
