using HopeLine.Service.Interfaces;
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

        public void OnGet(string pin = null,string user = null)
        {
            if (pin == null)
            {
                PIN = _communication.GenerateConnectionId();
            }
            else
            {
                PIN = pin;
            }

            if(user == null){
                UserName = "Test";

            }
            else{
                UserName = user;
            }
        }
    }
}