using HopeLine.DataAccess.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : Add props

    /// <summary>
    /// This is where the
    /// </summary>
    public class Conversation : BaseEntity
    {
        // this will serve as peerID for 
        // communication of mentor and user
        [Required]
        [StringLength(10)]
        public string PIN { get; set; }

        [Required]
        public MentorAccount Mentor { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public float Minutes { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfConversation { get; set; }

    }
}
