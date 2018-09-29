using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.Guest.Pages
{
    public class LoginModel : PageModel
    {
        public LoginModel()
        {
        }

        public string SessionId = "_guest";

        [BindProperty]
        [StringLength(40)]
        [MinLength(5)]
        public string Username { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if (Username != null)
            {
                HttpContext.Session.SetString("_guest", Username);
                TempData["user"] = Username;
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}