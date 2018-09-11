namespace HopeLine.DataAccess.Entities
{

    /// <summary>
    /// this class allows profile and language many to many rel
    /// </summary>
    public class ProfileLanguage
    {
        public int ProfileId { get; set; }

        public int LanguageId { get; set; }

        public Profile Profile { get; set; }

        public Language Language { get; set; }
    }
}
