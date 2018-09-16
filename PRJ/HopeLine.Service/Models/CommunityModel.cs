using HopeLine.Service.Models.Base;

namespace HopeLine.Service.Models
{
    //TODO : add props for community

    /// <summary>
    /// 
    /// </summary>
    public class CommunityModel : BaseModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string URL { get; set; }
        public string ImageURL { get; set; }
    }
}
