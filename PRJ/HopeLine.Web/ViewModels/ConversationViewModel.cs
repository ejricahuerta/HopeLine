using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HopeLine.DataAccess.Entities.Base;
using HopeLine.Service.Models;

namespace HopeLine.Web.ViewModels {
    //TODO : Add props

    /// <summary>
    /// This class will store conversation date time and users involved 
    /// as well as the id to connect user and mentor
    /// </summary>
    public class ConversationViewModel : BaseViewModel {

        public ConversationViewModel () {
            DateOfConversation = DateTime.UtcNow.ToString ();
        }

        [Required]
        [StringLength (30)]
        public string PIN { get; set; }
        public string UserId { get; set; }
        public string MentorId { get; set; }

        [DataType (DataType.DateTime)]
        public string DateOfConversation { get; set; }
        public Rating Rating { get; set; }
    }
}