
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{

    //TODO : create own Identity User
    public class HopeLineUser : IdentityUser
    {

        public enum Account
        {
            Guest, User, Mentor
        }
        public HopeLineUser()
        {

        }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]

        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string LastName { get; set; }


        public Account AccountType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }
    }
}
