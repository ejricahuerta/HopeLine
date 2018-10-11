using HopeLine.DataAccess.Entities;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ITokenService _tokenService;
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly SignInManager<HopeLineUser> _signInManager;

        public AuthController(ITokenService tokenService,UserManager<HopeLineUser> userManager, SignInManager<HopeLineUser> signInManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;

        }


        //TODO : needs to separate token builder and create new action for sending tokens
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username.Length < 6)
                {
                    return UnprocessableEntity("Username Invalid...");
                }

                if (model.IsGuest == false && model.Password.Length < 6)
                {
                    return UnprocessableEntity("Password Invalid...");
                }

                var token = await _tokenService.SignInUser(model.Username, model.Password, model.IsGuest);

                if (token != null)
                {
                    return Ok(token);
                }
            }
            return BadRequest("Unable to Login...");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new MentorAccount
                {
                    UserName = model.Username,
                    Email = model.Username
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var newuser = await _userManager.FindByEmailAsync(model.Username);
                   return Ok( _tokenService.GenerateToken(model.Username, newuser));

                }

            }
            return BadRequest("Unable to Process Registration...");
        }
    }
}
