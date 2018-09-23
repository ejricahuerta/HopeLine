using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.DataAccess.Entities;
using HopeLine.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Pages
{
    public class IndexModel : PageModel
    {   
        private readonly ICommonResource _commonResource;
        public IndexModel(ICommonResource commonResource)
        {
            _commonResource = commonResource;
        }

        public IList<String> Languages { get; set; }
        
        public void OnGet()
        {
        }
    }
}
