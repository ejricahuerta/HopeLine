using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HopeLine.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
    }

}
