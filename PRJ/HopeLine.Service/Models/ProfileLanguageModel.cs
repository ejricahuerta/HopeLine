using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HopeLine.Service.Models
{
    class ProfileLanguageModel
    {
        [Required]
        public int ProfileId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public ProfileModel Profile { get; set; }

        [Required]
        public LanguageModel Language { get; set; }
    }
}
}
