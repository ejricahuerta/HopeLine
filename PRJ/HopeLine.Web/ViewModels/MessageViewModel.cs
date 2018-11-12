using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel()
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