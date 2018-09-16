using HopeLine.Service.Models.Base;
using System.Collections.Generic;

namespace HopeLine.Service.Models
{
    //TODO  :   add props

    /// <summary>
    /// 
    /// </summary>
    public class ScheduleModel : BaseModel
    {
        public ICollection<ShiftModel> ShiftModels { get; set; }

        public string Notes { get; set; }
    }
}
