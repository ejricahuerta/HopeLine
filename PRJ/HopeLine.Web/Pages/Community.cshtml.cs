using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HopeLine.Web.ViewModels;
using HopeLine.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace HopeLine.Web.Pages
{
    public class CommunityModel : PageModel
    {
        private readonly ICommonResource _commonResource;
        public readonly IUserService _userService;
        public readonly UserManager<HopeLineUser> _userManager;

        public CommunityModel(ICommonResource commonResources, IUserService userService, UserManager<HopeLineUser> userManager)
        {
            _commonResource = commonResources;
            _userService = userService;
            _userManager = userManager;
        }

        [BindProperty]
        public bool isUser { get; set; }

        [BindProperty]
        public List<CommunityViewModel> Communities { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            HopeLineUser CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser != null)
            {
                if (CurrentUser.AccountType == DataAccess.Entities.Account.User || CurrentUser.AccountType == DataAccess.Entities.Account.Guest)
                {
                    isUser = true;
                }
                else
                {
                    isUser = false;
                }
            }
            else
            {
                isUser = true;
            }

            Communities = _commonResource.GetCommunities().Select(c => new CommunityViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                URL = c.URL,
                ImageURL = c.ImageURL
            }).ToList();

            return Page();
        }
    }
}