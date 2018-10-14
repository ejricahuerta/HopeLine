using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Pages
{
    public class ChatModel : PageModel
    {
        private readonly ICommunication _communication;
        private readonly ICommonResource _commonResource;

        public ChatModel(ICommunication communication, ICommonResource commonResource)
        {
            _commonResource = commonResource;
            _communication = communication;
        }

        [BindProperty]
        public string PIN { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        public IActionResult OnGet(string pin = null, string user = null)
        {

            // UserName = HttpContext.Session.GetString("_guest");
            // System.Console.WriteLine("User = " + UserName);
              
            // if (UserName != null)
            // {

                if (pin == null)
                {
                    PIN = _communication.GenerateConnectionId();
                }
                else
                {
                    PIN = pin;
                }
            //     return Page();
            // }
            // else
            // {
            //     string url = Url.Page("/Login", new { area = "Guest",returnUrl = "chat" });
            //     return LocalRedirect(url);
            // }
            return Page();
        }
    }
}