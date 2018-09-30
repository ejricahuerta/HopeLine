using HopeLine.DataAccess.Entities;
using HopeLine.Service.Models.Base;
using System.Collections.Generic;

namespace HopeLine.Service.Models
{
    // TODO : add props

    /// <summary>
    /// This class will hold the details about the language
    /// </summary>
    public class LanguageModel : BaseModel
    {
        public string Name { get; set; }
        
        public string CountryOrigin { get; set; }

        public ICollection<ProfileLanguage> ProfileLanguages { get; set; }

    }
}
