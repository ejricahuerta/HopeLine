
using HopeLine.Service.Configurations;

using System;
using System.Text;

namespace HopeLine.Security.Helpers
{
    //TODO : move all related jwt classes and func here
    public static class JWTHelpers
    {
        public static Microsoft.IdentityModel.Tokens.TokenValidationParameters TokenValidationParameters
        {
            get
            {
                return new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = APIConstant.URL,
                    ValidAudience = APIConstant.URL,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecretofGroup")),
                    ClockSkew = TimeSpan.Zero
                };
            }
        }

    }
}
