using HopeLine.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace HopeLine.Web.ViewModels
{

    /// <summary>
    /// this class allows the many to many rel for mentor and specialization
    /// </summary>
    public class MentorSpecializationViewModel : BaseViewModel


    {

        public MentorSpecializationViewModel()
        {
        }
        [Required]
        public string MentorAccountId { get; set; }

        [Required]
        public int SpecializationId { get; set; }

        [Required]
        public MentorAccountViewModel MentorAccount { get; set; }

        [Required]
        public SpecializationViewModel Specialization { get; set; }

    }
}
