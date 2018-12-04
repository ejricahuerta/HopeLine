using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities {

    public class Room {

        public Room () {
            DateAdded = DateTime.Now.ToString ("MM/dd/yyyy HH:mm:ss");
            Topics = new List<int> ();
        }

        [Key]
        public int Id { get; set; }
        public string MentorId { get; set; }
        public string RoomId { get; set; }
        public string GuestId { get; set; }
        public string DateAdded { get; set; }
        public IList<int> Topics { get; set; }
    }
}