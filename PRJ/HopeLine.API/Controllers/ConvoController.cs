using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HopeLine.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConvoController : ControllerBase
    {
        [Authorize]
        public IActionResult Check()
        {
            return Ok("VALID");
        }
    }
}
