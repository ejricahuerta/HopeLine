using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        //private readonly IUserService _userService;

        public AuthController(/*IUserService userService*/UserManager<HopeLineUser> userManager,
                                SignInManager<HopeLineUser> signInManager)
        {
            //_userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //TODO : Login

        //TODO : Logout

        //TODO : Register

        //TODO : Refresh Token
    }
}
