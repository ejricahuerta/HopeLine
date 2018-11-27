﻿using HopeLine.DataAccess.Entities;
using HopeLine.Service.Models.Base;
using System;
using System.Collections.Generic;

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
        public float Minutes { get; set; }
        public DateTime DateOfConversation { get; set; }

    }
}
