
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{

    //TODO : create own Identity User

    /// <summary>
    ///  This class is an extension of Identity provided by netcore 2 identity framework
    /// </summary>
    public class HopeLineUserViewModel : IdentityUser
    {
        /// <summary>
        /// These are types of user
        /// </summary>
        public enum Account
        {
            Admin, Mentor, User, Guest
        }
        public HopeLineUserViewModel()
        {
            DateAdded = DateTime.UtcNow.ToString();
            Activities = new List<ActivityViewModel>();
            if (AccountType == Account.Guest)
            {
                Profile = null;
            }
        }

        public Account AccountType { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public string DateAdded { get; set; }

        public ProfileViewModel Profile { get; set; }
        public ICollection<ActivityViewModel> Activities { get; set; }
    }
}
