using HopeLine.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// This class is about the Topics that the user wants to
    /// speak about with the mentor and chooses before they
    /// connect with them
    /// </summary>
    public class Topic : BaseEntity
    {
        public Topic()
        {
        }

        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
