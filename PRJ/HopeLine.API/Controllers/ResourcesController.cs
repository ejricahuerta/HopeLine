using Microsoft.AspNetCore.Mvc;

namespace HopeLine.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return NotFound();
        }
        //TODO : Add Community GET
        //TODO : Add Resources GET
    }
}
