using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{
    public class TopicViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
