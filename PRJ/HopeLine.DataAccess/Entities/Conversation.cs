using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HopeLine.DataAccess.Entities.Base;

namespace HopeLine.DataAccess.Entities {
    //TODO : Add props
    /// <summary>
    /// This class will store conversation date time and users involved 
    /// as well as the id to connect user and mentor
    /// </summary>
    public class Conversation : BaseEntity {
        public Conversation () {
            DateOfConversation = DateTime.Now.ToString ("MM/dd/yyyy HH:mm:ss");
        }

        [Required]
        [StringLength (30)]
        public string PIN { get; set; }

        [Required]
        public string MentorId { get; set; }

        [Required]
        public string UserId { get; set; }
        public float Minutes { get; set; }

        [DataType (DataType.DateTime)]
        public string DateOfConversation { get; set; }

    }
}