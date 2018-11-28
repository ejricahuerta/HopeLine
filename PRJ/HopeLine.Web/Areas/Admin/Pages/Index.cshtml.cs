using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HopeLine.Web.ViewModels;


namespace HopeLine.Web.Areas.Admin.Pages
{
    public class Index : PageModel
    {
        private readonly IUserService _userService;

       
        public Index(IUserService commonResources)
        {
            _userService = commonResources;
        }

    

        [BindProperty]
        public string QueryString { get; set; }

        [BindProperty]
        public List<UserViewModel> Users { get; set; }

        [BindProperty]
        public List<UserViewModel> Mentors { get; set; }

    

        public void OnGet()
        {
            Users = _userService.GetAllUsers().Select(c => new UserViewModel
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                AccountType = c.AccountType.ToString()

            }).ToList();
        }


        public IActionResult OnPostSearch()
        
        {

            Users = _userService.GetAllUsers().Where(u => u.FirstName.Contains(QueryString) || u.LastName.Contains(QueryString) || u.Email.Contains(QueryString)).Select(u=> new UserViewModel {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                AccountType = u.AccountType.ToString()

            }).ToList();

            return Page();
            
        }

        //public async Task<IActionResult> OnPostJoinListAsync() {




        //}




    }
}