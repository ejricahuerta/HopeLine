using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.User.Pages
{

    [AllowAnonymous]
    class LoginModel : PageModel
    {
        public LoginModel()
        {

        }

    }
}