using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Pages
{
    public class ChatModel : PageModel
    {
        private readonly ICommunication _communication;

        public ChatModel(ICommunication communication)
        {
            _communication = communication;
        }

        [BindProperty]
        public string PIN { get; set; }


        public void OnGet(string pin = null)
        {
            if (pin == null)
            {
                PIN = _communication.GenerateConnectionId();
            }
            else
            {
                PIN = pin;
            }
        }


    }
}