using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Helpers
{
    public  class AuthHelper : PageModel
    {
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly SignInManager<HopeLineUser> _signInManager;

        public AuthHelper(UserManager<HopeLineUser> userManager, SignInManager<HopeLineUser> signInManager)
        {
            _userManager = userManager;
            _signInManager  = signInManager;
        }
        public void Logout(HttpContext context = null){

            if(context != null){
                context.Session.Remove("_guest");
            }
        }   
    }
}