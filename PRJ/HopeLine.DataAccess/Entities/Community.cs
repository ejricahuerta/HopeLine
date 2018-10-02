using HopeLine.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// This class will hold links for all community that user can join and interact with
    /// </summary>
    public class Community : BaseEntity
    {

        public Community()
        {
            Name = "";
            Description = "";
            URL = "";
            ImageURL = "";
            //TODO: add the placeholder image here
            //ImageURL = "somestatic.file.link.png here";
        }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Url)]
        [StringLength(100)]
        public string URL { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; }
    }
}
