

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HopeLine.Web.ViewModels;


namespace HopeLine.Web.Pages
{
    public class HopeLineUser : PageModel
    {
        private readonly IUserService _userService;

        public HopeLineUser(IUserService commonResources)
        {
            _userService = commonResources;
        }

        [BindProperty]
        public List<UserViewModel> HopeLine { get; set; }

        public void OnGet()
        {
            HopeLine = _userService.GetAllUsers().Select(c => new UserViewModel
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                AccountType = c.AccountType.ToString()
            
            }).ToList();
        }
    }
}