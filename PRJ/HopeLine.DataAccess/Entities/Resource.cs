using HopeLine.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// 
    /// </summary>
    public class Resource : CommonEntity
    {
        public Resource()
        {
            //TODO : add the static place holder imagelink here
            //ImgUrl = "";
        }

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
