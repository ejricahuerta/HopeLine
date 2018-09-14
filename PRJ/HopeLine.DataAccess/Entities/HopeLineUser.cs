
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{

    //TODO : create own Identity User

    /// <summary>
    ///  This class is an extension of Identity provided by netcore 2 identity framework
    /// </summary>
    public class HopeLineUser : IdentityUser
    {
        /// <summary>
        /// These are types of user
        /// </summary>
        public enum Account
        {
            Admin, Mentor, User, Guest


        }
        public HopeLineUser()
        {
            DateAdded = DateTime.UtcNow;
            if (AccountType == Account.Guest)
            {
                Profile = null;
            }
        }

        public Account AccountType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }

        public Profile Profile { get; set; }
    }
}
