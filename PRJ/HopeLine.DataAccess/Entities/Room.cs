using System;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities {

    public class Room {

        public Room () {
            DateAdded = DateTime.Now.ToString ("MM/dd/yyyy HH:mm:ss");
        }

        [Key]
        public int Id { get; set; }
        public string MentorId { get; set; }
        public string RoomId { get; set; }
        public string GuestId { get; set; }
        public string DateAdded { get; set; }
    }
}