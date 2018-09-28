using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Web.ViewModels
{
    public class MapViewModel : BaseViewModel
    { 
        [Display(Name = "Loaction")]
        public ICollection<string> LocationNames { get; set; }

        public MapViewModel()
        {
            LocationNames = new List<string>();
        }
    }
}
