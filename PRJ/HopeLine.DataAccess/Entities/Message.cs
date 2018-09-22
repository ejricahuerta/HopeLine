using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
    public class Message
    {
        public Message()
        {
        
        }
        [Key]
        public int Id { get; set; }
    
        [Required]
        [StringLength(20)]
        public string ConnectionId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
    
        [Required]
        [StringLength(500)]
        public string Text { get; set; }
    }
}