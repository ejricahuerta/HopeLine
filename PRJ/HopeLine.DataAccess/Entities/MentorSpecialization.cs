namespace HopeLine.DataAccess.Entities
{

    /// <summary>
    /// this class allows the many to many rel for mentor and specialization
    /// </summary>
    public class MentorSpecialization
    {
        public string MentorAccountId { get; set; }
        public int SpecializationId { get; set; }
        public MentorAccount MentorAccount { get; set; }
        public Specialization Specialization { get; set; }

    }
}
