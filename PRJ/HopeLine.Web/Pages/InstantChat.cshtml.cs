using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.CoreServices;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Pages
{
    public class InstantChatModel : PageModel
    {
        private readonly ICommonResource _commonResource;
        private readonly ICommunication _communication;

        public InstantChatModel(ICommunication communication, ICommonResource commonResource)
        {
            _commonResource = commonResource;
            _communication = communication;
        }

        [BindProperty]
        public string PIN { get; set; }

        [BindProperty]
        public List<string> Topics { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public int IsUser { get; set; }
        public string ReturnUrl { get; set; }

        public IActionResult OnGet(string topics, string pin = null, string user = null)
        {
            UserName = HttpContext.Session.GetString("_guest");
            if (pin == null)
                PIN = _communication.GenerateConnectionId();
            else
                PIN = pin;

            if (UserName != null)
            {
                HttpContext.Session.SetString("_guest", UserName);

            }

            else
            {
                var name = "Guest" + Guid.NewGuid().ToString("N").Substring(0, 12);
                UserName = name;
                HttpContext.Session.SetString("_guest", UserName);
            }
            Topics = _commonResource.GetTopics().Select(t => t.Name).ToList();
            return Page();
        }

    }
}