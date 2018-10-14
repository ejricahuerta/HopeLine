using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.Mentor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<HopeLineUser> _signInManager;
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly IMessage _messageService;

        public LoginModel(SignInManager<HopeLineUser> signInManager, UserManager<HopeLineUser> userManager, IMessage messageService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _messageService = messageService;
        }

        [BindProperty]
        public LoginViewModel LoginInput { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            returnUrl = returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var res = await _signInManager.PasswordSignInAsync(LoginInput.Username, LoginInput.Password, true, false);
                if (res.Succeeded)
                {
                    
                    System.Console.WriteLine("User has logged in.");
                    return LocalRedirect(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login...");
            }
            return Page();
        }
    }
}