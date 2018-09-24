using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// This is per resource link
    /// Every link will have the link to go to the site, the name of the site and a picture of the site's logo
    /// The link also has an optional description
    /// </summary>
    public class Resource : BaseEntity
    {
        public Resource()
        {
            //TODO : add the static place holder imagelink here,
            //TODO : add a default pic of our application logo to all ImgUrl tags
            //ImgUrl = "";
        }

        [MaxLength(200)]
        public string Description { get; set; }

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
