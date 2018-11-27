using System.Collections.Generic;

namespace HopeLine.Web.ViewModels
{
    /// <summary>
    /// This class is for mentors who are hired by Admins
    /// </summary>
    public class MentorAccountViewModel : HopeLineUserViewModel
    {
        /// <summary>
        /// Initialize all default properties
        /// </summary>
        public MentorAccountViewModel()
        {
            AccountType = Account.Mentor;
            MentorSpecializations = new List<MentorSpecializationViewModel>();
            Conversations = new List<ConversationViewModel>();
        }

        //TODO : add more properties like availabilitie and Schedule

        public ICollection<ConversationViewModel> Conversations { get; set; }
        public ICollection<MentorSpecializationViewModel> MentorSpecializations { get; set; }

    }
}
