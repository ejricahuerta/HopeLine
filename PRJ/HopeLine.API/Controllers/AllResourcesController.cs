using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HopeLine.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AllResourcesController : ControllerBase
    {
        private readonly ILogger<AllResourcesController> _logger;
        private readonly ICommonResource _commonResource;

        public AllResourcesController(ILogger<AllResourcesController> logger, ICommonResource commonResource)
        {
            _logger = logger;
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
                _logger.LogInformation("Unable to get Topics");
                return NotFound();
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
                _logger.LogInformation("Unable to get Communities");
                return NotFound();
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

                _logger.LogInformation("Unable to get Resources");
                return NotFound();
            }
        }
    }
}
