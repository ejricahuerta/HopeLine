using HopeLine.DataAccess.Entities;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Models;
using HopeLine.Service.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        //private readonly ILogger _logger;

        //private readonly IUserService _userService;

        public AuthController(/*IUserService userService*/UserManager<HopeLineUser> userManager,
                                SignInManager<HopeLineUser> signInManager, ITokenService tokenService/*, ILogger logger*/)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            // _logger = logger;
        }


        //TODO : needs to separate token builder and create new action for sending tokens
        [HttpPost]
        public async Task<object> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsGuest)
                {
                    var temp = _userManager.Users.SingleOrDefault(u => u.UserName == APIConstant.UniversalEmail);
                    return _tokenService.GenerateToken(model.Username, temp);
                }
                else
                {

                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        var user = _userManager.Users.SingleOrDefault(u => u.Email == model.Username);
                        return _tokenService.GenerateToken(model.Username, user);
                    }
                }
            }
            return BadRequest("Unable to Login...");
        }

        [HttpPost]
        public async Task<object> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new GuestAccount
                {
                    Profile = new Profile
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName
                    },
                    UserName = model.Username,
                    Email = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return _tokenService.GenerateToken(model.Username, user);
                }
            }

            return BadRequest("Unable to Process Registration...");

        }
        //TODO : Refresh Token if needed


        //TODO : Logout


    }
}
