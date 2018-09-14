using HopeLine.DataAccess.Entities.Base;
using System;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// 
    /// </summary>
    public class Schedule : BaseEntity
    {
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan MyProperty { get; set; }
    }
}
