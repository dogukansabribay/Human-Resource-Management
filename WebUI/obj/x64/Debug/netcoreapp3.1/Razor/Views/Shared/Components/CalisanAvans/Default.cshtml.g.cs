#pragma checksum "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvans\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6ba0ebb7a7f5c49ba71bafd91c105db78f51a713"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_CalisanAvans_Default), @"mvc.1.0.view", @"/Views/Shared/Components/CalisanAvans/Default.cshtml")]
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
#line 1 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\_ViewImports.cshtml"
using WebUI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\_ViewImports.cshtml"
using WebUI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\_ViewImports.cshtml"
using Entities.Concrete.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\_ViewImports.cshtml"
using Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6ba0ebb7a7f5c49ba71bafd91c105db78f51a713", @"/Views/Shared/Components/CalisanAvans/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a79a8ff6601846e773eb80c86c5a0f2937b299d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_CalisanAvans_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<WebTestUI.Models.ViewModel.CalisanAvansViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"


<div class=""card border border-dark"">

<table class=""table"">
    <thead>
        <tr>
            <th>
               Avans Tarihi
            </th>
            <th>
               Avans Miktar??
            </th>

        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 20 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvans\Default.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                  ");
#nullable restore
#line 24 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvans\Default.cshtml"
             Write(Html.DisplayFor(modelItem => item.AvansTarihi));

#line default
#line hidden
#nullable disable
            WriteLiteral("       \r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 27 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvans\Default.cshtml"
               Write(Html.DisplayFor(modelItem => item.AvansMiktar??));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n\r\n                <td>\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 33 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvans\Default.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<WebTestUI.Models.ViewModel.CalisanAvansViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
