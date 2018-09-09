using HopeLine.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : Add props

    /// <summary>
    /// This is where the
    /// </summary>
    public class Conversation : BaseEntity
    {
        [Required]
        [StringLength(10)]
        public string PIN { get; set; }
    }
}
