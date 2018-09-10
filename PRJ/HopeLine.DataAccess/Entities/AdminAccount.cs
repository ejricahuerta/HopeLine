namespace HopeLine.DataAccess.Entities
{
    public class AdminAccount : HopeLineUser
    {
        public AdminAccount()
        {
            AccountType = Account.Admin;
        }
    }
}
