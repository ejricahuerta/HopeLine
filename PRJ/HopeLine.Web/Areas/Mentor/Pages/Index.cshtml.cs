using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using HopeLine.Web.Areas.Identity.Pages.Account.Manage;
using HopeLine.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using static HopeLine.Web.Areas.Identity.Pages.Account.ExternalLoginModel;

namespace HopeLine.Web.Areas.Mentor.Pages
{

    public class IndexModel : PageModel
    {
        /* For Profile */
        private readonly ICommunication _communication;
        private readonly ICommonResource _commonResource;
        private readonly IUserService _userService;
        private UserManager<HopeLine.Web.ViewModels.HopeLineUserViewModel> UserManager { get; set; }

        /*For Change Password*/
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly SignInManager<HopeLineUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public IndexModel(
            /* For Profile */
            ICommunication communication, ICommonResource commonResource, IUserService userService,
            /*For Change Password*/
            UserManager<HopeLineUser> userManager, SignInManager<HopeLineUser> signInManager, ILogger<ChangePasswordModel> logger)
        {
            /* For Profile */
            _commonResource = commonResource;
            _communication = communication;
            _userService = userService;

            /*For Change Password*/
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /* For Profile */
        [BindProperty]
        public string PIN { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public List<UserViewModel> Mentors { get; set; }

        [BindProperty]
        public UserViewModel CurrentMentor { get; set; }

        [BindProperty]
        public List<SpecializationViewModel> Specializations { get; set; }

        /*For Change Password*/
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        /* Password Change End */

        /*Conversation START*/
        [BindProperty]
        public List<ConversationViewModel> Conversations { get; set; }
        /*Conversation END*/

        /*Change Password START*/
        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
        /* For Change Password END */



        public async Task<IActionResult> OnGetAsync(string pin = null, string user = null)
        {
            var claim = User.Claims.FirstOrDefault(u => u.Type == "Account");
            var url = Url.Page("~/Index");

            HopeLineUser CurrentUser = await _userManager.GetUserAsync(User);

            CurrentMentor = new UserViewModel
            {
                Id = CurrentUser.Id,
                //FirstName = CurrentUser.Profile.FirstName,
                //LastName = CurrentUser.Profile.LastName,
                Username = CurrentUser.UserName,
                Email = CurrentUser.Email,
                AccountType = CurrentUser.AccountType.ToString(),
                Phone = CurrentUser.PhoneNumber
            };

            if (claim.Value == "User" || claim.Value == "Admin")
            {
                return Redirect(url);
            }
            /* Profile Page Logic START */
            Mentors = _userService.GetAllUsersByAccountType("Mentor").Select(m => new UserViewModel
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Username = m.Username,
                Email = m.Email,
                AccountType = m.AccountType,
                Phone = m.Phone

            }).ToList();


            Specializations = _userService.GetMentorSpecializations(CurrentMentor.Id).Select(s => new SpecializationViewModel
            {
                Name = s.Name,
                Description = s.Description
            }).ToList();

            // UserName = HttpContext.Session.GetString("_guest");
            // System.Console.WriteLine("User = " + UserName);

            // if (UserName != null)
            // {

            //     return Page();
            // }
            // else
            // {
            //     string url = Url.Page("/Login", new { area = "Guest",returnUrl = "chat" });
            //     return LocalRedirect(url);
            // }

            /* Profile Logic END */

            /* Change Password START */
            var user_ = await _userManager.GetUserAsync(User);
            if (user_ == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user_);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }
            /* Change Password END */

            /*Conversation Logic START*/
            Conversations = _userService.GetMentorConversations(CurrentMentor.Id).Select(c => new ConversationViewModel
            {
                Id = c.Id,
                PIN = c.PIN,
                UserId = c.UserId,
                UserName = c.UserName,
                Minutes = c.Minutes,
                DateOfConversation = c.DateOfConversation.ToString()
            }).ToList();
            /*Conversation Logic END */
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";


            Url.Page("/VideoChat", new { room = PIN });
            return RedirectToPage();
        }
    }
}