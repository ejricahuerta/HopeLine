using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HopeLine.Web.Pages
{

    public class ApplicantViewModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public bool IsVolunteer { get; set; }
    }
    public class CandidateModel : PageModel
    {
        private readonly ILogger<CandidateModel> _logger;
        private readonly IUserService _userService;

        [BindProperty]
        public ApplicantViewModel Applicant { get; set; }

        [BindProperty]
        public string Status { get; set; }
        public CandidateModel(ILogger<CandidateModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public IActionResult OnGet(string userId = null)
        {
            return Page();
        }
        public IActionResult OnPost(string userId = null)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (_userService.GetApplicants().FirstOrDefault(a => a.Email == Applicant.Email) != null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Application...");
                        return Page();
                    }
                    _userService.AddNewApplication(new ApplicantModel
                    {
                        FullName = Applicant.FullName,
                        Email = Applicant.Email,
                        Address = Applicant.Address,
                        Phone = Applicant.Phone,
                        IsVolunteer = Applicant.IsVolunteer
                    });
                    Status = "Success";
                    return Page();

                }
                catch (System.Exception)
                {

                    _logger.LogWarning("Unable to Process Application...");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Application...");
            return Page();
        }
    }
}