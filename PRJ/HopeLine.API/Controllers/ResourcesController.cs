using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HopeLine.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ICommonResource _commonResource;

        public ResourcesController(ICommonResource commonResource)
        {
            _commonResource = commonResource;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("some return");
        }

        [HttpGet]
        public IActionResult GetTopics()
        {
            try
            {

                return Ok(_commonResource.GetTopics());
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Unable to get Data from controller:", ex);
            }
        }
    }
}
