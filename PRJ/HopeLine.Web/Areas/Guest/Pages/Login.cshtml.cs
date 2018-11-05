using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.Guest.Pages {
    public class LoginModel : PageModel {
        private readonly ICommunication _communicationService;
        public LoginModel (ICommunication communicationService) {
            _communicationService = communicationService;
        }
        public string SessionId = "_guest";

        [BindProperty]
        [StringLength (40)]
        [MinLength (5)]
        public string Username { get; set; }
        public string ReturnUrl { get; set; }
        public IActionResult OnGet (string returnUrl = null) {
            ReturnUrl = returnUrl != null ? Url.Content ("~/" + returnUrl) : Url.Content ("~/");

            if (Username != null) {
                HttpContext.Session.SetString ("_guest", Username);
                TempData["user"] = Username;
            } else {
                var user = "Guest" + Guid.NewGuid ().ToString ("N").Substring (0, 12);
                Username = user;
                HttpContext.Session.SetString ("_guest", Username);
            }
            return Redirect (ReturnUrl);
        }

        public IActionResult OnPost (string returnUrl = null) {

            ReturnUrl = returnUrl != null ? Url.Content ("~/" + returnUrl) : Url.Content ("~/");

            if (Username != null) {
                HttpContext.Session.SetString ("_guest", Username);
                TempData["user"] = Username;
            } else {
                var user = "Guest" + Guid.NewGuid ().ToString ("N").Substring (0, 12);
                Username = user;
                HttpContext.Session.SetString ("_guest", Username);
            }
            return Redirect (ReturnUrl);
        }
    }
}