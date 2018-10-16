using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    public class OnlineMentor
    {
        public OnlineMentor()
        {
            
        }
        [Key]
        public int Id { get; set; }
        public bool Available { get; set; }
        public string ConnectionId { get; set; }
        public string MentorId { get; set; }
    }
}