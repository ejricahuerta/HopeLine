using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;

namespace HopeLine.DataAccess.Entities
{
    //TODO : Add props
    public class Specialization : BaseEntity
    {
        public string Description { get; set; }
        public ICollection<MentorAccount> Mentors { get; set; }
        public ICollection<Topic> Topics { get; set; }

        public Specialization()
        {
            Mentors = new List<MentorAccount>();
            Topics = new List<Topic>();
        }
    }
}
