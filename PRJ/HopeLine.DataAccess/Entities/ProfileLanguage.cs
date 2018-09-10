namespace HopeLine.DataAccess.Entities
{
    public class ProfileLanguage
    {
        public int ProfileId { get; set; }

        public int LanguageId { get; set; }

        public Profile Profile { get; set; }

        public Language Language { get; set; }
    }
}
