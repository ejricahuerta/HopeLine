using System;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    public class Message
    {
        public Message()
        {
            DateAdded = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }
        [Required]
        [StringLength(30)]
        public string RoomId { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; }
    }
}