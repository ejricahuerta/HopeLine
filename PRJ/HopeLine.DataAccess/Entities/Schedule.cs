using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;

namespace HopeLine.DataAccess.Entities
{
    //TODO : needs refactoring and annotaions

    /// <summary>
    /// This class will hold the schedule of time and details
    /// on when the Mentor will work
    /// </summary>
    public class Schedule : BaseEntity
    {
        //TODO : need to decide about this class func - Eduardo
        public bool Available { get; set; }
        public ICollection<Shift> Shifts { get; set; }

    }
}
