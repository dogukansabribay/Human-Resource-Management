#pragma checksum "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cd83c4e07fe5ca50ea381754775a2789681b9277"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_SirketYonetici_Views_Home_Index), @"mvc.1.0.view", @"/Areas/SirketYonetici/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\_ViewImports.cshtml"
using WebUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\_ViewImports.cshtml"
using WebUI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\_ViewImports.cshtml"
using Entities.Concrete.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\_ViewImports.cshtml"
using Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cd83c4e07fe5ca50ea381754775a2789681b9277", @"/Areas/SirketYonetici/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a79a8ff6601846e773eb80c86c5a0f2937b299d", @"/Areas/SirketYonetici/Views/_ViewImports.cshtml")]
    public class Areas_SirketYonetici_Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml"
 if (Context.Request.Cookies["Username"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <span>\r\n        ");
#nullable restore
#line 9 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml"
   Write(Context.Request.Cookies["Username"].ToUpper());

#line default
#line hidden
#nullable disable
            WriteLiteral(" Hoş geldiniz...\r\n    </span>\r\n");
#nullable restore
#line 11 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container col-6\" style=\"margin-top:50px\">\r\n    <div class=\"text-center\">\r\n        <h3>Yaklaşan Doğum Günleri</h3>\r\n    </div>\r\n    ");
#nullable restore
#line 17 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml"
Write(await Component.InvokeAsync("CalisanDogumGunu"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n<div class=\"container col-6\">\r\n    <div class=\"text-center\">\r\n        <h3>Üyelik Planı</h3>\r\n    </div>\r\n    ");
#nullable restore
#line 23 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml"
Write(await Component.InvokeAsync("SirketinUyelikPlani"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n\r\n\r\n");
#nullable restore
#line 28 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml"
Write(await Component.InvokeAsync("DogumGunuKutlama"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 29 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml"
Write(await Component.InvokeAsync("ResmiTatilKutlama"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 30 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Areas\SirketYonetici\Views\Home\Index.cshtml"
Write(await Component.InvokeAsync("IzinGunuBildirim"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591