using HopeLine.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// This class will hold the schedule of time and details
    /// on when the Mentor will work
    /// </summary>
    public class Schedule : BaseEntity
    {
        //TODO : need to decide about this class func - Eduardo

        public bool Availible { get; set; }

        public ICollection<Shift> Shifts { get; set; }

        public string Notes { get; set; }
    }
}
