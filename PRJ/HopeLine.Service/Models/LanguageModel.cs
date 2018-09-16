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
        public string Region { get; set; }

        public string LanguageName { get; set; }

        public ICollection<ProfileModel> ProfileModels { get; set; }

    }
}
