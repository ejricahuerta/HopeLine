using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Pages.Account
{
    [Authorize(Policy = "UserOnly")]
    public class ConfirmEmailModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}