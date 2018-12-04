using System;
using System.Collections.Generic;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Models.Base;

namespace HopeLine.Service.Models
{
    //TODO : add props

    /// <summary>
    /// 
    /// </summary>
    public class ConversationModel : BaseModel
    {
        public string PIN { get; set; }
        public string MentorId { get; set; }
        public string UserId { get; set; }
        public DateTime DateOfConversation { get; set; }
        public Rating Rating { get; set; }
    }
}