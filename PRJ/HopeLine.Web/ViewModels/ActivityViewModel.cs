using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Web.ViewModels
{
    public class ActivityViewModel : BaseViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public string DateOfActivity { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000)]
        public string Description { get; set; }
    }
}
