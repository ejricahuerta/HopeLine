using HopeLine.Service.Models.Base;
using System.Collections.Generic;

namespace HopeLine.Service.Models
{
    //TODO : add props 

    /// <summary>
    /// 
    /// </summary>
    public class SpecializationModel : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<TopicModel> TopicModels { get; set; }
    }
}
