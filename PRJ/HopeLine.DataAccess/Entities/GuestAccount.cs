namespace HopeLine.DataAccess.Entities
{
    public class GuestAccount : HopeLineUser
    {
        public GuestAccount()
        {
            AccountType = Account.Guest;
        }

    }
}
