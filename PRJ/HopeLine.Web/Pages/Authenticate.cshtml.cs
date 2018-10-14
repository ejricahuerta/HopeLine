using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Pages
{
    public class AuthenticateModel : PageModel
    {
        public IActionResult OnGet(string returnUrl = null)
        {


            returnUrl = returnUrl ?? Url.Content("~/");

            if (HttpContext.Session.GetString("_guest") != null)
            {
                return Redirect(returnUrl);
            }
            return Page();
        }
    }
}