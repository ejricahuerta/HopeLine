using System.Collections.Generic;

namespace HopeLine.Service.Models
{



    public interface IUserModel
    {
        string Id { get; set; }
    }

    /// <summary>
    /// DTO fo users. this does not contain any logic at all.
    /// it is a Dummy data transfer object
    /// </summary>
    public class UserModel : IUserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public ICollection<string> Languages { get; set; }
        public string AccountType { get; set; }
    }

}
