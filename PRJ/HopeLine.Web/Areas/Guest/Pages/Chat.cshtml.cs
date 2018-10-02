using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopeLine.Service.CoreServices;
using HopeLine.Service.Interfaces;
using HopeLine.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HopeLine.Web.Areas.Guest.Pages
{
    public class ChatModel : PageModel
    {
        private readonly ICommonResource _commonResource;
        private readonly ICommunication _communication;
        public ChatModel(ICommunication communication, ICommonResource commonResource)
        {
            _commonResource = commonResource;
            _communication = communication;
        }
        public void OnGet()
        {
        }
    }
}