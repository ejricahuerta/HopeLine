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


        //TODO: put me back to where I was
        const string twilioAccountSid = "AC9a143e097bc86d459251da871c6f1bc5";
        const string twilioApiKey = "SKf1fd0ea9a3e3d530efd1c76f53c3db08";
        const string twilioApiSecret = "RARbIc5RQS63xThPOKxaQTX088PPd2Pl";


        [BindProperty]
        public string Token { get; set; }
        [BindProperty]
        public string UserId { get; set; }

        public VideoChatModel(ICommunication communicationService)
        {
            _communicationService = communicationService;

        }

        [BindProperty]
        public string RoomId { get; set; }
        public IActionResult OnGet(string roomId = null, string userId = null)
        {
            try
            {
                if (userId == null || roomId == null)
                {
                    Redirect("/Index");
                }
                else
                {
                    RoomId = roomId;
                    UserId = userId;
                    GetTwilioToken();
                    return Page();
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            return NotFound();
        }

        // get twilio token 
        private void GetTwilioToken()
        {
            var grant = new VideoGrant();
            grant.Room = RoomId;
            var grants = new HashSet<IGrant> { grant };

            var identity = UserId; //TODO : change this
                                   // Create an Access Token generator
            var token = new Token(twilioAccountSid, twilioApiKey, twilioApiSecret, identity, grants: grants);

            Console.Write("Id is: " + UserId + " token is: " + token);

            //var token = new Token(APIConstant.TwilioAccountSID,
            //                        APIConstant.TwilioApiSID,
            //                        APIConstant.TwilioSecret,
            //                        identity: identity,
            //                        grants: grants);
            Token = token.ToJwt();
            //Console.WriteLine("Here is the token: " + token.ToJwt());
            // Serialize the token as a JWT
            Console.WriteLine("Here is the token from the CS" + token.ToJwt());

        }
        public IActionResult OnPost(string roomId = null, string userId = null)
        {
            try
            {
                if (userId == null || roomId == null)
                {
                    Redirect("/Index");
                }
                else
                {
                    RoomId = roomId;
                    UserId = userId;
                    GetTwilioToken();
                    return Page();
                }
            }
            catch (System.Exception ex)
            {
                return NotFound();
            }
            return Redirect("/Index");
        }
    }
}