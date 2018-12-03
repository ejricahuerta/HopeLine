using System;
using System.Collections.Generic;
using System.Text;

namespace HopeLine.DataAccess.Entities.Base {
    public class Shift : BaseEntity {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}