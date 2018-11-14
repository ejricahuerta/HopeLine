using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{

    /// <summary>
    /// this class allows profile and language many to many rel
    /// </summary>
    public class ProfileLanguageViewModel
    {
        [Required]
        public int ProfileId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public ProfileViewModel Profile { get; set; }

        [Required]
        public LanguageViewModel Language { get; set; }
    }
}
