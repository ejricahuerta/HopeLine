using HopeLine.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// This class will hold links for 
    /// </summary>
    public class Community : CommonEntity
    {
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
