namespace HopeLine.DataAccess.Entities
{
    public class MentorSpecialization
    {
        public string MentorAccountId { get; set; }
        public int SpecializationId { get; set; }
        public MentorAccount MentorAccount { get; set; }
        public Specialization Specialization { get; set; }

    }
}
