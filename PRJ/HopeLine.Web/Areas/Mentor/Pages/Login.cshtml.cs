using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.Mentor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<HopeLineUser> _signInManager;
        private readonly UserManager<HopeLineUser> _userManager;

        public LoginModel(SignInManager<HopeLineUser> signInManager, UserManager<HopeLineUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [BindProperty]
        public LoginViewModel LoginInput { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "Invalid Login!";
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(LoginInput.Username);

            if (user != null)
            {

                var result = await _signInManager.CheckPasswordSignInAsync(user, LoginInput.Password, false);
                if (result.Succeeded)
                {
                    System.Console.WriteLine("Logged In...");
                    return Redirect( Url.Content("~/Index"));
                }
            }

            return Page();

        }

    }
}