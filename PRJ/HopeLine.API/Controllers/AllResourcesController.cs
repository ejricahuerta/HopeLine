using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HopeLine.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AllResourcesController : ControllerBase
    {
        private readonly ICommonResource _commonResource;

        public AllResourcesController(ICommonResource commonResource)
        {
            _commonResource = commonResource;
        }

        [HttpGet]

        public IActionResult Topics()
        {
            try
            {

                return Ok(_commonResource.GetTopics());
            }
            catch (System.Exception ex)
            {
                return NotFound(ex);
            }

        }

        [HttpGet]
        public IActionResult Communities()
        {
            try
            {
                return Ok(_commonResource.GetCommunities());
            }
            catch (System.Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet]
        public IActionResult Resources()
        {
            try
            {
                return Ok(_commonResource.GetResources());
            }
            catch (System.Exception ex)
            {

                return NotFound(ex);
            }
        }
    }
}
