using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{

    /// <summary>
    /// this class allows profile and language many to many rel
    /// </summary>
    public class ProfileLanguage
    {
        [Required]
        public int ProfileId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public Profile Profile { get; set; }

        [Required]
        public Language Language { get; set; }
    }
}
