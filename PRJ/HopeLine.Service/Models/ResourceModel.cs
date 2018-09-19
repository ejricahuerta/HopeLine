using HopeLine.Service.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.Service.Models
{
    //TODO : add props

    /// <summary>
    /// 
    /// </summary>
    public class ResourceModel : BaseModel
    {
        public string Description { get; set; }

        public string Name { get; set; }
        
        public string URL { get; set; }
        
        public string ImageURL { get; set; }
    }
}
