using HopeLine.Service.Config;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace HopeLine.Security.Helpers
{
    //TODO : move all related jwt classes and func here
    public static class JWTHelpers
    {
        public static TokenValidationParameters TokenValidationParameters
        {
            get
            {
                return new TokenValidationParameters
                {
                    ValidIssuer = APIConstant.URL,
                    ValidAudience = APIConstant.URL,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecretofGroup")),
                    ClockSkew = TimeSpan.Zero
                };
            }
        }

    }
}
