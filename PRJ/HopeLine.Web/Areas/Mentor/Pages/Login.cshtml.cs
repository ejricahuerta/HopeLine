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
        private readonly SignInManager<MentorAccount> _signInManager;
        private readonly UserManager<MentorAccount> _userManager;

        public LoginModel(SignInManager<MentorAccount> signInManager, UserManager<MentorAccount> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [BindProperty]
        public LoginViewModel LoginInput { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(model.Username);

            if (user != null)
            {

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    return RedirectToPage("~/");
                }
            }

            return Page();

        }

    }
}