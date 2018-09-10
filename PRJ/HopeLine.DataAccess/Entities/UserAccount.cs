namespace HopeLine.DataAccess.Entities
{
    public class UserAccount : HopeLineUser
    {
        public UserAccount()
        {
            AccountType = Account.Mentor;
        }
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}
