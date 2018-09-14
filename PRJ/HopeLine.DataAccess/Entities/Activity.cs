using HopeLine.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HopeLine.DataAccess.Entities
{
    public class Activity : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}
