#pragma checksum "D:\StudyWork\GitHub\HopeLine\PRJ\HopeLine.Web\Pages\Account\ForgotPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9857d3363228d4b96febcfcacf5bb22446d235a3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(HopeLine.Web.Pages.Account.Pages_Account_ForgotPassword), @"mvc.1.0.razor-page", @"/Pages/Account/ForgotPassword.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure.RazorPageAttribute(@"/Pages/Account/ForgotPassword.cshtml", typeof(HopeLine.Web.Pages.Account.Pages_Account_ForgotPassword), null)]
namespace HopeLine.Web.Pages.Account
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\StudyWork\GitHub\HopeLine\PRJ\HopeLine.Web\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 2 "D:\StudyWork\GitHub\HopeLine\PRJ\HopeLine.Web\Pages\_ViewImports.cshtml"
using HopeLine.Web;

#line default
#line hidden
#line 3 "D:\StudyWork\GitHub\HopeLine\PRJ\HopeLine.Web\Pages\_ViewImports.cshtml"
using HopeLine.DataAccess;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9857d3363228d4b96febcfcacf5bb22446d235a3", @"/Pages/Account/ForgotPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"803fb01550e69568b018d3558639c7b709c6d29e", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Account_ForgotPassword : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "D:\StudyWork\GitHub\HopeLine\PRJ\HopeLine.Web\Pages\Account\ForgotPassword.cshtml"
  
    ViewData["Title"] = "ForgotPassword";
    Layout = "~/Pages/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(159, 29, true);
            WriteLiteral("\r\n<h2>ForgotPassword</h2>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<HopeLine.Web.Pages.Account.ForgotPasswordModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<HopeLine.Web.Pages.Account.ForgotPasswordModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<HopeLine.Web.Pages.Account.ForgotPasswordModel>)PageContext?.ViewData;
        public HopeLine.Web.Pages.Account.ForgotPasswordModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
