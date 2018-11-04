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
            if (HttpContext.Session.GetString ("_guest") != null) {
                return Redirect (Url.Content ("~/"));
            }

            ReturnUrl = returnUrl != null ? Url.Content ("~/" + returnUrl) : Url.Content ("~/");
            return Page ();
        }

        public IActionResult OnPost (string returnUrl = null) {
            ReturnUrl = returnUrl != null ? Url.Content ("~/" + returnUrl) : Url.Content ("~/");

            string pin = "";
            if (ReturnUrl.ToLower ().Contains ("chat")) {
                pin = _communicationService.GenerateConnectionId ();
                ReturnUrl = Url.Content ("~/" + returnUrl + "?pin=" + pin);
            }

            if (Username != null) {
                HttpContext.Session.SetString ("_guest", Username);
                TempData["user"] = Username;
                return Redirect (ReturnUrl);
            }
            return Page ();
        }
    }
}