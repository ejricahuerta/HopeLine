using HopeLine.DataAccess.Entities.Base;
using System;

namespace HopeLine.DataAccess.Entities
{
    //TODO : needs refactoring and annotaions

    /// <summary>
    /// 
    /// </summary>
    public class Schedule : BaseEntity
    {
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }

        public DateTime LogoutTime { get; set; }
        public float TotalHours { get; set; }
    }
}
