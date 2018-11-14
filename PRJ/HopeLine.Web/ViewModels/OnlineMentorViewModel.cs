using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    public class OnlineMentorViewModel
    {
        public OnlineMentorViewModel()
        {

        }
        [Key]
        public int Id { get; set; }
        public bool Available { get; set; }
        public string ConnectionId { get; set; }
        public string MentorId { get; set; }
    }
}