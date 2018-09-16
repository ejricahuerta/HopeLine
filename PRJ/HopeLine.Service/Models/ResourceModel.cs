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
        public string Name { get; set; }
        
        public string Url { get; set; }
        
        public string ImgUrl { get; set; }
    }
}
