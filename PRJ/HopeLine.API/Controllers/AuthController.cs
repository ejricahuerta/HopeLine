using HopeLine.DataAccess.Entities;
using HopeLine.Security.Interfaces;
using HopeLine.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.Encodings.Web;
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
        private readonly ILogger<AuthController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ITokenService _tokenService;
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly SignInManager<HopeLineUser> _signInManager;

        public AuthController(ILogger<AuthController> logger, IEmailSender emailSender, ITokenService tokenService, UserManager<HopeLineUser> userManager, SignInManager<HopeLineUser> signInManager)
        {
            _logger = logger;
            _emailSender = emailSender;
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
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Page(
                      "/Account/ResetPassword",
                      pageHandler: null,
                      values: new { userId = user.Id, code = code },
                      protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Username, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    var newuser = await _userManager.FindByEmailAsync(model.Username);
                    var claimres = await _userManager.AddClaimAsync(newuser, new Claim("Account", "Mentor"));

                    return Ok();
                }

            }
            return BadRequest("Unable to Process Registration...");
        }
    }
}
