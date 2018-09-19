using HopeLine.DataAccess.Entities;
using HopeLine.Security.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HopeLine.Security.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITokenService
    {

        object GenerateToken(string username, HopeLineUser user);
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string expiredToken);

        Task<object> SignInUser(string username, string password, bool isguest);
        Task<object> RegisterUser(RegisterModel model);
    }
}
