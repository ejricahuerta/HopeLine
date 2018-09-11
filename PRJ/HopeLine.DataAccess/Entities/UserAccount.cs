namespace HopeLine.DataAccess.Entities
{

    /// <summary>
    /// Registered user account with profile
    /// </summary>
    public class UserAccount : HopeLineUser
    {
        public UserAccount()
        {
            AccountType = Account.User;
        }
        public Profile Profile { get; set; }
    }
}
