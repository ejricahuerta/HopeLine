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

            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, LoginInput.Username),
                    new Claim(ClaimTypes.Email, LoginInput.Username),
                    new Claim(ClaimTypes.Role, "Mentor"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. Required when setting the 
                    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    // set with AddCookie. Also required when setting 
                    // ExpiresUtc.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                var res = await _signInManager.PasswordSignInAsync(LoginInput.Username, LoginInput.Password, false, false);
                if (res.Succeeded)
                {
                    var mentor = await _userManager.FindByEmailAsync(LoginInput.Username);
                    await _messageService.NewMentorAvailable(mentor.Id);       
                    await HttpContext.SignInAsync(
                        "Cookies",
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return Redirect(returnUrl);
                }
            }

            return Page();
        }
    }
}