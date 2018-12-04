using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HopeLine.Web.Pages
{

    public class CandidateViewModel
    {
        [Required]
        [StringLength(40)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool IsVolunteer { get; set; }
    }
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ICommonResource _commonResource;
        public readonly IUserService _userService;
        public readonly UserManager<HopeLineUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, IEmailSender emailSender, ICommonResource commonResource, IUserService userService, UserManager<HopeLineUser> userManager)
        {
            _logger = logger;
            _emailSender = emailSender;
            _commonResource = commonResource;
            _userService = userService;
            _userManager = userManager;
        }


        [BindProperty]
        public CandidateViewModel Candidate { get; set; }
        public IList<string> Languages { get; set; }

        [BindProperty]
        public string Status { get; set; }

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


        public async Task<IActionResult> OnPostCandidateAsync()
        {
            if (ModelState.IsValid)
            {

                var callbackUrl = Url.Page(
                                        "/Candidate",
                                        pageHandler: null,
                                        values: new { userId = Candidate.Email },
                                        protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Candidate.Email, "Confirm your email",
                    $"Please Fill Up the form by clicking this link <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                Status = "Success";
                return Page();
            }
            Status = "Failed";
            return Page();
        }
        public IActionResult OnPost()
        {
            _logger.LogInformation("Redirecting to Instant Chat");
            return LocalRedirect(Url.Page("/instantChat"));
        }

    }
}