using HopeLine.Service.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.Service.Models
{
    //TODO : add props 

    /// <summary>
    /// 
    /// </summary>
    public class SpecializationModel : BaseModel
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public ICollection<TopicModel> TopicModels { get; set; }

        //public ICollection<MentorSpecialization> MentorSpecializations { get; set; }
    }
}
