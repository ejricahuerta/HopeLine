namespace HopeLine.DataAccess.Entities
{
    /// <summary>
    /// Admin account
    /// Testing
    /// </summary>
    public class AdminAccount : HopeLineUser
    {
        public AdminAccount()
        {
            AccountType = Account.Admin;
        }
    }
}
