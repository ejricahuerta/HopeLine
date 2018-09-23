using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Web.ViewModels
{
    public class SpecializationViewModel : BaseViewModel
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public ICollection<TopicViewModel> TopicViewModels { get; set; }

        public SpecializationViewModel()
        {
            TopicViewModels = new List<TopicViewModel>();
        }
    }
}
