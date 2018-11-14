using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;

namespace HopeLine.Web.ViewModels
{
    //TODO : needs refactoring and annotaions

    /// <summary>
    /// This class will hold the schedule of time and details
    /// on when the Mentor will work
    /// </summary>
    public class ScheduleViewModel : BaseViewModel
    {
        //TODO : need to decide about this class func - Eduardo

        public bool Available { get; set; }

        public ICollection<Shift> Shifts { get; set; }

    }
}
