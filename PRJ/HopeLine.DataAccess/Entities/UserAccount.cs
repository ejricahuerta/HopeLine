using HopeLine.DataAccess.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HopeLine.DataAccess.Entities
{
   public class UserAccount : HopeLineUser
    {
        public UserAccount()
        {
            AccountType = Account.Mentor;
        }

    }
}
