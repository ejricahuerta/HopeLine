using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Configurations;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twilio.Jwt.AccessToken;

namespace HopeLine.Web.Pages
{
    public class VideoChatModel : PageModel
    {
        private readonly ICommunication _communicationService;

        [BindProperty]
        public string Token { get; set; }

        public VideoChatModel(ICommunication communicationService)
        {
            _communicationService = communicationService;

        }

        [BindProperty]
        public string RoomId { get; set; }

        public IActionResult OnGet(string roomId = null)
        {
            try
            {
                if (roomId != null && GetTwilioToken())
                {
                    RoomId = roomId;
                    return Page();
                }
                else
                {
                    return Redirect("/Index");
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                return NotFound();
            }
        }

        // get twilio token 
        private bool GetTwilioToken()
        {
            try
            {
                var grant = new VideoGrant();
                grant.Room = "cool room";
                var grants = new HashSet<IGrant> { grant };
                var identity = "example-user"; //TODO : change this
                // Create an Access Token generator
                var token = new Token(APIConstant.TwilioAccountSID,
                                        APIConstant.TwilioApiSID,
                                        APIConstant.TwilioSecret,
                                        identity: identity,
                                        grants: grants);
                Token = token.ToJwt();
                Console.WriteLine("Here is the token: " + token.ToJwt());
                // Serialize the token as a JWT
                Console.WriteLine(token.ToJwt());
                if (Token == null)
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;

        }
    }
}