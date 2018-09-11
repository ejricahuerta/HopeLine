using HopeLine.DataAccess.Entities;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HopeLine.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly SignInManager<HopeLineUser> _signInManager;
        private readonly ITokenService _tokenService;

        //private readonly IUserService _userService;

        public AuthController(/*IUserService userService*/UserManager<HopeLineUser> userManager,
                                SignInManager<HopeLineUser> signInManager, ITokenService tokenService)
        {
            //_userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        [HttpPost]
        public async Task<object> Login([FromBody] UserModel user)
        {
            var userLogginIn = await _userManager.FindByEmailAsync(user.Email);

            if (userLogginIn != null)
            {
                var result = await _signInManager.PasswordSignInAsync(userLogginIn, user.Password, false, false);

                if (result.Succeeded)
                {
                    return _tokenService.GenerateJwtToken(user.Email, userLogginIn);
                }

            }
            throw new ApplicationException("INVALID LOGIN!");

        }

        //TODO : Login

        //TODO : Logout

        //TODO : Register

        //TODO : Refresh Token
    }
}
