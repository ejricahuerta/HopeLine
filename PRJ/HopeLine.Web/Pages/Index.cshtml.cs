using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HopeLine.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICommonResource _commonResource;
        public readonly IUserService _userService;
        public readonly UserManager<HopeLineUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, ICommonResource commonResource, IUserService userService, UserManager<HopeLineUser> userManager)
        {
            _logger = logger;
            _commonResource = commonResource;
            _userService = userService;
            _userManager = userManager;
        }

        public IList<string> Languages { get; set; }

        [BindProperty]
        public IList<TopicViewModel> Topics { get; set; }

        [BindProperty]
        public bool isUser { get; set; }

        [BindProperty]
        public IList<TopicViewModel> TopicsSelected { get; set; }
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

            Topics = _commonResource.GetTopics().Select(t => new TopicViewModel
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            _logger.LogInformation("Redirecting to Instant Chat");
            return LocalRedirect(Url.Page("/instantChat"));
        }

    }
}