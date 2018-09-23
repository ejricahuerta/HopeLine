using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Web.ViewModels
{
    public class ConversationViewModel : BaseViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(30)]
        public string PIN { get; set; }

        [Required]
        public string MentorId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Duration)]
        public float Minutes { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfConversation { get; set; }
    }
}
