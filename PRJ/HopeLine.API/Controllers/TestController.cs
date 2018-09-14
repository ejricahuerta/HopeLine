using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HopeLine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserService _userService;

        public TestController(IUserService userService)
        {
            _userService = userService;
        }

        //localhost:5000/api/Test/users?apikey=1234
        // GET: api/Test
        [HttpGet("users")]

        public IActionResult Users([FromQuery]string apikey)
        {
            if (apikey.Equals("1234"))
            {

                return Ok(_userService.GetAllUsers());
            }
            return BadRequest("Enter your api key");

        }

        //localhost:5000/api/Test/usersByType?apikey=1234&userType=User
        [HttpGet("usersbytype")]

        public IActionResult Users([FromQuery]string apikey, [FromQuery]string userType)
        {

            if (apikey.Equals("1234"))
            {

                return Ok(_userService.GetAllUsersByAccountType(userType));
            }
            return BadRequest("Enter your api key");

        }


    }
}
