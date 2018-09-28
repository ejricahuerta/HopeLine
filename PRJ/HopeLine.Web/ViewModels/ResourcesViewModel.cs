using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Web.ViewModels
{
    public class ResourcesViewModel : BaseViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImgUrl { get; set; }
    }
}
