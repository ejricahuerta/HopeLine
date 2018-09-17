using HopeLine.Service.Models.Base;
using System.Collections.Generic;

namespace HopeLine.Service.Models
{
    //TODO : add props for map

    /// <summary>
    /// 
    /// </summary>
    public class MapModel : BaseModel
    {
        public ICollection<string> LocationNames { get; set; }
        
    }
}
