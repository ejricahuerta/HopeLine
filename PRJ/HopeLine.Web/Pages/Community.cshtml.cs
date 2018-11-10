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
    public class CommunityModel : PageModel
    {
        private readonly ICommonResource _commonResource;

        public CommunityModel(ICommonResource commonResources)
        {
            _commonResource = commonResources;
        }

        [BindProperty]
        public List<CommunityViewModel> Communities { get; set; }

        public void OnGet()
        {
            Communities = _commonResource.GetCommunities().Select(c => new CommunityViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                URL = c.URL,
                ImageURL = c.ImageURL
            }).ToList();
        }
    }
}