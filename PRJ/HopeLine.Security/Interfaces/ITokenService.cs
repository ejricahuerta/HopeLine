using HopeLine.DataAccess.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HopeLine.Security.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITokenService
    {
        Task<ClaimsIdentity> GetClaimsIdentity(string username, string password);
        Task<object> GenerateToken(string email, HopeLineUser user);
    }
}
