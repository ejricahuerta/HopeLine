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
        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("_guest") != null){
                return Redirect(Url.Content("~/"));
            }
            return Page();
        }
    }
}