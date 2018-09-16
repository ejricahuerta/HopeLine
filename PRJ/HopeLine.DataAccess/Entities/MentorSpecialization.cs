using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{

    /// <summary>
    /// this class allows the many to many rel for mentor and specialization
    /// </summary>
    public class MentorSpecialization
    {
        [Required]
        public string MentorAccountId { get; set; }

        [Required]
        public int SpecializationId { get; set; }

        [Required]
        public MentorAccount MentorAccount { get; set; }

        [Required]
        public Specialization Specialization { get; set; }

    }
}
