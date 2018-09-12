using HopeLine.DataAccess.Entities;
using System.Security.Claims;

namespace HopeLine.Security.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITokenService
    {
        object GenerateToken(string username, HopeLineUser user);
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string expiredToken);
    }
}
