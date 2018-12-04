using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace HopeLine.Web.Pages
{

    public class ResourcesModel : PageModel
    {
        public readonly ICommonResource _commonResource;
        public readonly IUserService _userService;
        public readonly UserManager<HopeLineUser> _userManager;

        [BindProperty]
        public List<ResourcesViewModel> Resources { get; set; }

        [BindProperty]
        public List<ResourcesViewModel> DepressionRes { get; set; }
        [BindProperty]
        public List<ResourcesViewModel> AnxietyRes { get; set; }
        [BindProperty]
        public List<ResourcesViewModel> BullyingRes { get; set; }
        [BindProperty]
        public List<ResourcesViewModel> SuicideRes { get; set; }

        [BindProperty]
        public bool isUser { get; set; }


        public ResourcesModel(ICommonResource commonResource, IUserService userService, UserManager<HopeLineUser> userManager)
        {
            _commonResource = commonResource;
            _userService = userService;
            _userManager = userManager;

            Resources = new List<ResourcesViewModel>();
            DepressionRes = new List<ResourcesViewModel>();
            AnxietyRes = new List<ResourcesViewModel>();
            BullyingRes = new List<ResourcesViewModel>();
            SuicideRes = new List<ResourcesViewModel>();
        }


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

            Resources = _commonResource.GetResources().Select(r => new ResourcesViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                URL = r.URL,
                ImageURL = r.ImageURL
            }).ToList();

            foreach (var i in Resources)
            {
                if (i.Id > 99 && i.Id < 200)
                    DepressionRes.Add(i);
                else if (i.Id > 199 && i.Id < 300)
                    AnxietyRes.Add(i);
                else if (i.Id > 299 && i.Id < 400)
                    BullyingRes.Add(i);
                else if (i.Id > 399 && i.Id < 500)
                    SuicideRes.Add(i);
            }

            return Page();
        }
    }
}