using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Pages
{
    public class MapModel : PageModel
    {
        private readonly ICommonResource _commonResource;
        public readonly IUserService _userService;
        public readonly UserManager<HopeLineUser> _userManager;

        public MapModel(ICommonResource commonResources, IUserService userService, UserManager<HopeLineUser> userManager)
        {
            _commonResource = commonResources;
            _userService = userService;
            _userManager = userManager;
        }

        [BindProperty]
        public bool isUser { get; set; }

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

            return Page();
        }
    }
}