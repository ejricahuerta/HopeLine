//using HopeLine.Security.Interfaces;


using HopeLine.DataAccess.Entities;
using HopeLine.Security.Helpers;
using HopeLine.Security.Interfaces;
using HopeLine.Service.Config;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HopeLine.Security.Services
{
    public class TokenService : ITokenService
    {

        /// <summary>
        /// generate bearer token for auth controller
        /// </summary>
        /// <param name="username"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public object GenerateToken(string username, HopeLineUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecretofGroup"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(30));


            var claims = this.CreateClaims(user);
            // TODO : string const must be inside appsettings
            var token = new JwtSecurityToken(
                APIConstant.URL,
                APIConstant.URL,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        /// <summary>
        /// this will only be implemented when refresh toekn is needed
        /// </summary>
        /// <param name="expiredToken"></param>
        /// <returns></returns>
        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string expiredToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var claimsPrincipal = tokenHandler.ValidateToken(expiredToken, JWTHelpers.TokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return claimsPrincipal;
        }


        /// <summary>
        /// create claims for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private List<Claim> CreateClaims(HopeLineUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            return claims;
        }
    }
}
