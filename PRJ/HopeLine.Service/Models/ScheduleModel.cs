using HopeLine.Service.Models.Base;
using System;

namespace HopeLine.Service.Models
{
    //TODO  :   add props and refactor

    /// <summary>
    /// 
    /// </summary>
    public class ScheduleModel : BaseModel
    {
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }

        public DateTime LogoutTime { get; set; }
        public float TotalHours { get; set; }
    }
}
