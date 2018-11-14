namespace HopeLine.Web.ViewModels
{
    /// <summary>
    /// Guest Account that only holds username
    /// </summary>
    public class GuestAccountViewModel : HopeLineUserViewModel
    {
        public GuestAccountViewModel()
        {
            AccountType = Account.Guest;
        }

    }
}
