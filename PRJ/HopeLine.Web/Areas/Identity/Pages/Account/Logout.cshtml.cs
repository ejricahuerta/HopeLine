using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using HopeLine.Service.Interfaces;

namespace HopeLine.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<HopeLineUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IMessage _messageService;

        public LogoutModel(SignInManager<HopeLineUser> signInManager, ILogger<LogoutModel> logger, IMessage messageService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _messageService = messageService;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            bool isGuest = true;
            var user = HttpContext.Session.GetString("_guest");
            if (user == null)
            {
                isGuest = false;
                user = _signInManager.UserManager.GetUserName(User);
            }

            var room = _messageService.GetRoomForUser(user, isGuest);
            System.Console.WriteLine("Room is : " + room);
            await _messageService.DeleteAllMessages(room);
            HttpContext.Session.Clear();
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                returnUrl = Url.Content("~/");
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
    }
}