
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.DataAccess.Entities
{

    //TODO : create own Identity User

    /// <summary>
    /// 
    /// </summary>
    public class HopeLineUser : IdentityUser
    {

        public enum Account
        {
            User, Mentor
        }
        public HopeLineUser()
        {

        }


        public Account AccountType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAdded { get; set; }
    }
}
