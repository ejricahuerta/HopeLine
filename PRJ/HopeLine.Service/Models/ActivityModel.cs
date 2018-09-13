using HopeLine.Service.Models.Base;

namespace HopeLine.Service.Models
{
    public class ActivityModel : BaseModel
    {
        public string DateOfActivity { get; set; }
        public string Comment { get; set; }
    }
}
