using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Web.ViewModels
{
    public class LanguageViewModel : BaseViewModel
    {
        [Required]
        [StringLength(50)]
        public string Region { get; set; }

        [Display(Name = "Language")]
        public string LanguageName { get; set; }
    }
}
