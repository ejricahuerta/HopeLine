using HopeLine.DataAccess.Entities;
using HopeLine.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HopeLine.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly SignInManager<HopeLineUser> _signInManager;


        //private readonly IUserService _userService;

        public AuthController(/*IUserService userService*/UserManager<HopeLineUser> userManager,
                                SignInManager<HopeLineUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }


        //TODO : needs to separate token builder and create new action for sending tokens
        [HttpPost]
        public async Task<object> Login([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = _userManager.Users.SingleOrDefault(u => u.Email == model.Username);
                return GenerateToken(model.Username, user);
            }
            throw new ApplicationException("UNTRACED ERROR!");
        }


        [HttpPost]
        public async Task<object> Register([FromBody] RegisterModel model)
        {
            var user = new UserAccount
            {
                UserName = model.Username,
                Email = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return await GenerateToken(model.Username, user);
            }
            throw new ApplicationException("UNTRACED ERROR!");

        }


        //TODO : move to security prj
        private async Task<object> GenerateToken(string username, HopeLineUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SomeSecretofGroup7"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(30));


            // TODO : string const must be inside appsettings
            var token = new JwtSecurityToken(
                "https://localhost:5000",
                "https://localhost:5000",
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        //TODO : Logout

        //TODO : Register

        //TODO : Refresh Token
    }
}
