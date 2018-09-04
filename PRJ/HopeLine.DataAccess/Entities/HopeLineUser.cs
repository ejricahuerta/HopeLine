using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HopeLine.DataAccess.Entities
{

    public class HopeLineUser : IdentityUser
    {
        
        [Required]
        [MinLength(2)]
        [MaxLength(20)]

        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(10)]
        public String AccountType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }
    }
}
