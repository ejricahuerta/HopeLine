using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// this class will hold saved maps
    /// </summary>
    public class Map : CommonEntity
    {
        public Map()
        {
            Url = "";
            LocationNames = null;
        }

        [Required]
        [MinLength(2)]
        [MaxLength(500)]
        public string Url { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(500)]
        public ICollection<string> LocationNames { get; set; }
    }
}
