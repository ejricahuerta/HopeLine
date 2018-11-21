using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Pages
{
    public class VideoChatModel : PageModel
    {
        private readonly ICommunication _communicationService;

        public VideoChatModel(ICommunication communicationService)
        {
            _communicationService = communicationService;
        }

        [BindProperty]
        public string  RoomId  { get; set; }

        public void OnGet()
        {
            RoomId = _communicationService.GenerateConnectionId();
        }
    }
}