using System;
using System.Collections.Generic;
using System.Text;

namespace HopeLine.DataAccess.Entities.Base
{
    public class Shift : BaseEntity
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
