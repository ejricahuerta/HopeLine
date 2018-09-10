using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : Add props

    /// <summary>
    /// 
    /// </summary>
    public class Language : BaseEntity
    {
        public Language()
        {
            ProfileLanguages = new List<ProfileLanguage>();
        }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(40)]
        public string CountryOrigin { get; set; }

        public ICollection<ProfileLanguage> ProfileLanguages { get; set; }
    }
}
