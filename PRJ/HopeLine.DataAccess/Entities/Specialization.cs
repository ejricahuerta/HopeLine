using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    //TODO : Add props
    public class Specialization : BaseEntity
    {

        [Required]
        public string Description { get; set; }

        [Required]
        public ICollection<MentorAccount> Mentors { get; set; }
        
        [Required]
        public ICollection<Topic> Topics { get; set; }

        public Specialization()
        {
            Mentors = new List<MentorAccount>();
            Topics = new List<Topic>();
        }
    }
}
