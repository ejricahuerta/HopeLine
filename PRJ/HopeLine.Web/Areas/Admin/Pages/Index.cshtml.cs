using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HopeLine.Web.ViewModels;
using System.ComponentModel.DataAnnotations;
using HopeLine.DataAccess.Entities;
using HopeLine.Web.Areas.Identity.Pages.Account.Manage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using static HopeLine.Web.Areas.Identity.Pages.Account.ExternalLoginModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using HopeLine.Security.Models;

namespace HopeLine.Web.Areas.Admin.Pages
{

    [Authorize(Policy = "AdminOnly")]
    
    public class Index : PageModel
    {
        private readonly IUserService _userService;
        private readonly ICommunication _communication;
        
        /*For Change Password*/
        private readonly UserManager<HopeLineUser> _userManager;
        private readonly SignInManager<HopeLineUser> _signInManager;
        private readonly IEmailSender _emailsender;
        private readonly ILogger<ChangePasswordModel> _logger;

        public Index(IUserService commonResources, ICommunication communication, IEmailSender emailsender)
        {
            _userService = commonResources;
            _communication = communication;
            _emailsender = emailsender;
        }

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        [BindProperty]
        public RegisterModel RegisterModel { get; set; }

        [BindProperty]
       public InputModel Input { get; set; }

        [BindProperty]
        public List<ConversationViewModel> Conversations { get; set; }

        [BindProperty]
        public string QueryString { get; set; }

        [BindProperty]
        public List<UserViewModel> Users { get; set; }

        [BindProperty]
        public List<UserViewModel> Mentors { get; set; }
        [BindProperty]
        public List<SpecializationViewModel> Specializations { get; set; }
      


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

        public async Task<IActionResult> OnPostAddMentorAsync()
        {
            Console.WriteLine("The Email is: " +  RegisterViewModel.Username);
            if (ModelState.IsValid)
            {
                try
                {
                    var profile = new Profile
                    {
                        FirstName = RegisterViewModel.FirstName,
                        LastName = RegisterViewModel.LastName,

                    };

                    //TODO: include language
                    var userAcc = new MentorAccount
                    {
                        UserName = RegisterViewModel.Username,
                        Email = RegisterViewModel.Username,
                        PhoneNumber = RegisterViewModel.Phone,
                        SIN = RegisterViewModel.SIN,
                        Profile = profile

                    };
                    var result = await _userManager.CreateAsync(userAcc);
                        //await _userManager.CreateAsync(userAcc, RegisterViewModel.Password);
                    if (result.Succeeded)
                    {
                        /// IEmailSender neeeded
                        System.Console.WriteLine("New Account Created");
                        var code = await _userManager.GeneratePasswordResetTokenAsync(userAcc);
                            //_userManager.GenerateEmailConfirmationTokenAsync(userAcc);

                        var callbackUrl = Url.Page(
                             "/Account/ResetPassword",
                            pageHandler: null,
                            values: new { userId = userAcc.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailsender.SendEmailAsync(RegisterViewModel.Username, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (System.Exception) {
                    System.Console.WriteLine("Add mentor not working!");
                }
            }
            System.Console.WriteLine("Unable to Add User...");
            return Page();
        }
        //public async Task<IActionResult> OnGetAsync(string pin = null, string user = null, string returnUrl = null)
        public async Task<IActionResult> OnGetAsync(string pin = null, string user = null, string returnUrl = null)
        {
            returnUrl = returnUrl = returnUrl ?? Url.Page("/Index", new { area = "Admin" });
            var url = Url.Page("~/Index");
            // returnUrl = returnUrl ?? Url.Content("~/");

            //HopeLineUser CurrentUser = await _userManager.GetUserAsync(User);

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


            /* Profile Logic END */

            /* Change Password START */
            //var user_ = await _userManager.GetUserAsync(User);
            //if (user_ == null)
            //{
            //    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            //}

            //var hasPassword = await _userManager.HasPasswordAsync(user_);
            //if (!hasPassword)
            //{
            //    return RedirectToPage("./SetPassword");
            //}
            /* Change Password END */

            /*Conversation Logic START*/
            Conversations = _communication.GetConversations().Select(c => new ConversationViewModel
            {
                Id = c.Id,
                PIN = c.PIN,
                UserId = c.UserId,
                //MentorId = c.MentorId,
                Minutes = c.Minutes,
                DateOfConversation = c.DateOfConversation.ToString()
            }).ToList();
            System.Console.WriteLine("Convo count = " + Conversations.Count());
            foreach (var c in Conversations)
            {
                System.Console.WriteLine("User: " + c.UserId);
                System.Console.WriteLine("mentor: " + c.MentorId);
            }


            Users = _userService.GetAllUsers().Select(c => new UserViewModel
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                AccountType = c.AccountType.ToString()

            }).ToList();


            //HopeLineUser CurrentUser = await _userManager.GetUserAsync(User);

            //CurrentMentor = new UserViewModel
            //{
            //    Id = CurrentUser.Id,
            //    Username = CurrentUser.UserName,
            //    Email = CurrentUser.Email,
            //    AccountType = CurrentUser.AccountType.ToString(),
            //    Phone = CurrentUser.PhoneNumber
            //};

            /* Profile Page Logic START */
            //Mentors = _userService.GetAllUsersByAccountType("Mentor").Select(m => new UserViewModel
            //{
            //    Id = m.Id,
            //    FirstName = m.FirstName,
            //    LastName = m.LastName,
            //    Username = m.Username,
            //    Email = m.Email,
            //    AccountType = m.AccountType,
            //    Phone = m.Phone

            //}).ToList();


            /* Profile Logic END */

            /* Change Password START */
            //var user_ = await _userManager.GetUserAsync(User);
            //if (user_ == null)
            //{
            //    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            //}

            //var hasPassword = await _userManager.HasPasswordAsync(user_);
            //if (!hasPassword)
            //{
            //    return RedirectToPage("./SetPassword");
            //}
            /* Change Password END */

            /*Conversation Logic START*/
            //Conversations = _communication.GetConversationsByMentorId(CurrentMentor.Email).Select(c => new ConversationViewModel
            //{
            //    Id = c.Id,
            //    PIN = c.PIN,
            //    UserId = c.UserId,
            //    //MentorId = c.MentorId,
            //    Minutes = c.Minutes,
            //    DateOfConversation = c.DateOfConversation.ToString()
            //}).ToList();
            //System.Console.WriteLine("Convo count = " + Conversations.Count());
            //foreach (var c in Conversations)
            //{
            //    System.Console.WriteLine("User: " + c.UserId);
            //    System.Console.WriteLine("mentor: " + c.MentorId);
            //}
            //    /*Conversation Logic END */
            return Page();
        }




    }
}