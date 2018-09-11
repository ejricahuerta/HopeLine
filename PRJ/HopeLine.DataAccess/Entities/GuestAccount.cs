namespace HopeLine.DataAccess.Entities
{
    /// <summary>
    /// Guest Account that only holds username
    /// </summary>
    public class GuestAccount : HopeLineUser
    {
        public GuestAccount()
        {
            AccountType = Account.Guest;
        }

    }
}
