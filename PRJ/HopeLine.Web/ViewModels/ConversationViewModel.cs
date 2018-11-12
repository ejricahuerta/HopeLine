using HopeLine.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    //TODO : Add props

    /// <summary>
    /// This class will store conversation date time and users involved 
    /// as well as the id to connect user and mentor
    /// </summary>
    public class ConversationViewModel : BaseViewModel
    {

        public ConversationViewModel()
        {
            DateOfConversation = DateTime.UtcNow.ToString();;
            LanguageUsed = new List<LanguageViewModel>();
        }
        // this will serve as peerID for 
        // communication of mentor and user
        [Required]
        [StringLength(10)]
        public string PIN { get; set; }

        [Required]
        public MentorAccountViewModel Mentor { get; set; }

        public string UserName { get; private set; }

        public string UserId { get; private set; }

        public float Minutes { get; set; }

        [DataType(DataType.DateTime)]
        public string DateOfConversation { get; set; }

        public ICollection<LanguageViewModel> LanguageUsed { get; set; }
        public void SetUser(string id, string name)
        {
            UserId = id;
            UserName = name;
        }
    }
}
