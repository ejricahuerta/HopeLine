using HopeLine.DataAccess.Entities;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        private readonly ITokenService _tokenService;


        //private readonly IUserService _userService;

        public AuthController(/*IUserService userService*/UserManager<HopeLineUser> userManager,
                                SignInManager<HopeLineUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        //TODO : needs to separate token builder and create new action for sending tokens
        [HttpPost]

        public async Task<object> Login([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = _userManager.Users.SingleOrDefault(u => u.Email == model.Username);
                return _tokenService.GenerateToken(model.Username, user);
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
                return _tokenService.GenerateToken(model.Username, user);
            }
            throw new ApplicationException("UNTRACED ERROR!");

        }



        //TODO : Refresh Token if needed


        //TODO : Logout


    }
}
