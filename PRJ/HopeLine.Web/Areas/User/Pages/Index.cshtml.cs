using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.User.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICommunication _communication;
        private readonly ICommonResource _commonResource;
        private readonly IUserService _userService;

        public IndexModel(ICommunication communication, ICommonResource commonResource, IUserService userService)
        {
            /* For Profile */
            _commonResource = commonResource;
            _communication = communication;
            _userService = userService;
        }

        /* For Profile */
        [BindProperty]
        public string PIN { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public List<UserViewModel> Users { get; set; }

        [BindProperty]
        public UserViewModel CurrentUser { get; set; }

        [BindProperty]
        public List<ActivityViewModel> Activities { get; set; }

        public async Task<IActionResult> OnGetAsync(string pin = null, string user = null)
        {
            var claim = User.Claims.FirstOrDefault(u => u.Type == "Account");
            var url = Url.Page("~/Index");

            if (claim.Value == "Mentor" || claim.Value == "Admin")
            {
                return Redirect(url);
            }
            /* Profile Page Logic START */
            Users = _userService.GetAllUsersByAccountType("User").Select(m => new UserViewModel
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Username = m.Username,
                Email = m.Email,
                AccountType = m.AccountType,
                Phone = m.Phone

            }).ToList();

            return Page();
            /*
            Activities = _userService.GetMentorSpecializations("1a68265d-9601-47a7-9539-fe4a9486fb65").Select(s => new SpecializationViewModel
            {
                Name = s.Name,
                Description = s.Description
            }).ToList();*/
        }
    }
}