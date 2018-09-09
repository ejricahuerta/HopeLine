using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HopeLine.DataAccess.Entities
{
    class Profile
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]

        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }
        public ICollection<Language> Languages { get; set; }

        public Profile()
        {
            Languages = new List<Language>();
        }
    }
}
