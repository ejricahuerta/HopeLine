using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.CoreServices;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HopeLine.Web.Pages {
    public class InstantChatModel : PageModel {
        private readonly ICommonResource _commonResource;
        private readonly ICommunication _communication;

        public InstantChatModel (ICommunication communication, ICommonResource commonResource) {
            _commonResource = commonResource;
            _communication = communication;
        }

        [BindProperty]
        public string PIN { get; set; }

        [BindProperty]
        public List<string> Topics { get; set; }

        [BindProperty]
        public string UserName { get; set; }
        public string ReturnUrl { get; set; }

        public IActionResult OnGet (string topics, string pin = null, string user = null) {
            UserName = "guest";
            Topics = JsonConvert.DeserializeObject<List<string>>(topics);
            if(Topics == null)
            {
                System.Diagnostics.Debug.WriteLine(Topics + "Topics are NULL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(Topics + "Topics Received!!!!!!!!!!!!!!!!!!!");
            }

            if (UserName != null && Topics != null) {
                if (pin == null)
                    PIN = _communication.GenerateConnectionId ();
                else
                    PIN = pin;

                return Page ();

            } else {
                string url = Url.Page ("/Login", new { area = "Guest", returnUrl = "chat" });
                return LocalRedirect (url);
            }
        }

    }
}