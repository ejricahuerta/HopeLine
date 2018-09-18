using HopeLine.Security.Interfaces;
using HopeLine.Security.Models;
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


        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;

        }


        //TODO : needs to separate token builder and create new action for sending tokens
        [HttpPost]
        public async Task<object> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await _tokenService.SignInUser(model.Username, model.Password, model.IsGuest);

                if (token != null)
                {
                    return token;
                }
            }
            return BadRequest("Unable to Login...");
        }


        [HttpPost]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_tokenService.RegisterUser(model));
            }
            return BadRequest("Unable to Process Registration...");
        }
    }
}
