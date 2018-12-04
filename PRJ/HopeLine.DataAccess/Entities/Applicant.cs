using System;
using System.ComponentModel.DataAnnotations;
using HopeLine.DataAccess.Entities.Base;

namespace HopeLine.DataAccess.Entities
{
    public class Applicant : BaseEntity
    {
        public Applicant()
        {
            IsVolunteer = true;
            DateAdded = DateTime.Now.ToString();
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string FullName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public bool IsVolunteer { get; set; }
    }
}