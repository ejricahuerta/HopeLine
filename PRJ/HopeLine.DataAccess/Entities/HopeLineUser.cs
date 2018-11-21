
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{
        public enum Account
        {
            Admin, Mentor, User, Guest
        }

    //TODO : create own Identity User

    /// <summary>
    ///  This class is an extension of Identity provided by netcore 2 identity framework
    /// </summary>
    public class HopeLineUser : IdentityUser
    {
        /// <summary>
        /// These are types of user
        /// </summary>
        public HopeLineUser()
        {
            DateAdded = DateTime.UtcNow.ToString();
            Activities = new List<Activity>();
            if (AccountType == Account.Guest)
            {
                Profile = null;
            }
        }

        public Account AccountType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string DateAdded { get; set; }

        public Profile Profile { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}
