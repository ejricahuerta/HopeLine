using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.Guest.Pages
{
    public class LoginModel : PageModel
    {
        public LoginModel()
        {
            
        }        

        [BindProperty]
        public string Username { get; set; }
        public void OnGet()
        {
             Username = "Sample";
        } 

    }
}