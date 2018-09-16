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
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Url { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(500)]
        public string ImgUrl { get; set; }
    }
}
