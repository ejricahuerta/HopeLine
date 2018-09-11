using HopeLine.DataAccess.Entities;

namespace HopeLine.Security.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITokenService
    {
        object GenerateJwtToken(string email, HopeLineUser user);
    }
}
