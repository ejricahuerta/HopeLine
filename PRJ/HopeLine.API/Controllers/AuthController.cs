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
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.FirstName == null &&
                    model.LastName == null &&
                    model.FirstName.Length < 2 &&
                    model.LastName.Length < 2)
                {
                    return UnprocessableEntity("Invalid Name...");
                }

                return Ok(_tokenService.RegisterUser(model));
            }
            return BadRequest("Unable to Process Registration...");
        }
    }
}
