using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels {

    public class RoomViewModel {

        [Key]
        public int Id { get; set; }
        public string MentorId { get; set; }
        public string RoomId { get; set; }
        public string GuestId { get; set; }
    }
}