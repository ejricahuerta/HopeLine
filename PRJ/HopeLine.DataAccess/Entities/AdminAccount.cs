namespace HopeLine.DataAccess.Entities
{
    /// <summary>
    /// Admin account 
    /// </summary>
    public class AdminAccount : HopeLineUser
    {
        public AdminAccount()
        {
            AccountType = Account.Admin;
        }
    }
}
