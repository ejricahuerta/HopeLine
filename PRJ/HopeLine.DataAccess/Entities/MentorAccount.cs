﻿using System.Collections.Generic;

namespace HopeLine.DataAccess.Entities
{
    /// <summary>
    /// This class is for mentors who are hired by Admins
    /// </summary>
    public class MentorAccount : HopeLineUser
    {
        /// <summary>
        /// Initialize all default properties
        /// </summary>
        public MentorAccount()
        {
            AccountType = Account.Mentor;
            MentorSpecializations = new List<MentorSpecialization>();
            Conversations = new List<Conversation>();
        }

        //TODO : add more properties like availabilitie and Schedule

        public ICollection<Conversation> Conversations { get; set; }
        public ICollection<MentorSpecialization> MentorSpecializations { get; set; }
    }
}
