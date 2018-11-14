using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Pages
{
    
    public class ResourcesModel : PageModel
    {
        public readonly ICommonResource _commonResource;

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


        public ResourcesModel(ICommonResource commonResource)
        {
            _commonResource = commonResource;
            Resources = new List<ResourcesViewModel>();
            DepressionRes = new List<ResourcesViewModel>();
            AnxietyRes = new List<ResourcesViewModel>();
            BullyingRes = new List<ResourcesViewModel>();
            SuicideRes = new List<ResourcesViewModel>();
        }

        
        public void OnGet()
        {
            Resources = _commonResource.GetResources().Select(r => new ResourcesViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                URL = r.URL,
                ImageURL = r.ImageURL
            }).ToList();

            foreach(var i in Resources)
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
        }
    }
}