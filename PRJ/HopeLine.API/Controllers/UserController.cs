using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Authorize]
[ApiController]
public class UserController : ControllerBase {
public UserController(IUserService)
{
    
}

}