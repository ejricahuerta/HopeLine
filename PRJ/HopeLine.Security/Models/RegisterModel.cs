namespace HopeLine.Security.Models
{
    public class RegisterModel : LoginModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

    }
}
