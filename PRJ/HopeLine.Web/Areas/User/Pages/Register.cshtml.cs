using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.User.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {

        public RegisterModel()
        {

        }
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}