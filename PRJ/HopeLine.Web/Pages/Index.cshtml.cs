using HopeLine.Service.Interfaces;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace HopeLine.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICommonResource _commonResource;
        public IndexModel(ICommonResource commonResource)
        {
            _commonResource = commonResource;
        }

        public IList<string> Languages { get; set; }

        [BindProperty]
        public IList<TopicViewModel> Topics { get; set; }

        [BindProperty]
        public IList<TopicViewModel> TopicsSelected { get; set; }
        public IActionResult OnGet()
        {

            Topics = _commonResource.GetTopics().Select(t => new TopicViewModel
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            // TempData["Selected"] = TopicsSelected.Select(t => t.Name).ToList();
            return LocalRedirect(Url.Page("/instantChat"));
        }

    }
}