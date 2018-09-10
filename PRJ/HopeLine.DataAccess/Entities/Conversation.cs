using HopeLine.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : Add props

    /// <summary>
    /// This class will store conversation date time and users involved 
    /// as well as the id to connect user and mentor
    /// </summary>
    public class Conversation : BaseEntity
    {

        public Conversation()
        {
            DateOfConversation = DateTime.UtcNow;
            LanguageUsed = new List<Language>();
        }
        // this will serve as peerID for 
        // communication of mentor and user
        [Required]
        [StringLength(10)]
        public string PIN { get; set; }

        [Required]
        public MentorAccount Mentor { get; set; }

        public string UserName { get; private set; }

        public string UserId { get; private set; }

        public float Minutes { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfConversation { get; set; }

        public ICollection<Language> LanguageUsed { get; set; }
        public void SetUser(string id, string name)
        {
            UserId = id;
            UserName = name;
        }
    }
}
