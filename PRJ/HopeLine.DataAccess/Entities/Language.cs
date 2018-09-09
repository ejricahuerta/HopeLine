using HopeLine.DataAccess.Entities.Base;

namespace HopeLine.DataAccess.Entities
{
    //TODO : Add props

    /// <summary>
    /// 
    /// </summary>
    public class Language : BaseEntity
    {
        public string LanguageName { get; set; }
        public string CountryOrigin { get; set; }
    }
}
