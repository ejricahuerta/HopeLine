using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.User.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly SignInManager<HopeLineUser> _signInManager;
        private readonly IEmailSender _emailsender;

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        [BindProperty]
        public string RetypePassword { get; set; }
        public RegisterModel(UserManager<HopeLineUser> userManager, SignInManager<HopeLineUser> signInManager, IEmailSender emailsender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailsender = emailsender;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {

                var profile = new Profile
                {
                    FirstName = RegisterViewModel.FirstName,
                    LastName = RegisterViewModel.LastName
                };

                //TODO: include language
                var user = new UserAccount
                {
                    UserName = RegisterViewModel.Username,
                    Email = RegisterViewModel.Username,
                    Profile = profile

                };

                var result = await _userManager.CreateAsync(user, RegisterViewModel.Password);
                if (result.Succeeded)
                {
                    /// IEmailSender neeeded
                    System.Console.WriteLine("New Account Created");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                         "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailsender.SendEmailAsync(RegisterViewModel.Username, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    System.Console.WriteLine("Redirectin to Index..");
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            System.Console.WriteLine("Unable to Add User...");
            return Page();
        }
    }
}