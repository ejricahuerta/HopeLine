using System.Collections.Generic;
using static HopeLine.DataAccess.Entities.HopeLineUser;

namespace HopeLine.Service.Models
{

    /// <summary>
    /// DTO fo users. this does not contain any logic at all.
    /// it is a Dummy data transfer object
    /// </summary>
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public ICollection<string> Languages { get; set; }
        public Account AccountType { get; set; }
    }

}
