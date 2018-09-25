using HopeLine.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopeLine.Web.ViewModels
{
    public class ScheduleViewModel : BaseViewModel
    {
        public ICollection<ShiftViewModel> ShiftViewModels { get; set; }

        public string Notes { get; set; }

        public ScheduleViewModel()
        {
            ShiftViewModels = new List<ShiftViewModel>();
        }
    }
}
