using HopeLine.Service.Models.Base;

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
    }
}
