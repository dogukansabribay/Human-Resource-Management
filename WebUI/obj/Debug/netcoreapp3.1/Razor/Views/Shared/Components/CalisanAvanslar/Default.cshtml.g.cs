#pragma checksum "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "18b240f7c2a3a12c0265db08cda43063f78ceb61"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_CalisanAvanslar_Default), @"mvc.1.0.view", @"/Views/Shared/Components/CalisanAvanslar/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"18b240f7c2a3a12c0265db08cda43063f78ceb61", @"/Views/Shared/Components/CalisanAvanslar/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a79a8ff6601846e773eb80c86c5a0f2937b299d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_CalisanAvanslar_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DataAccess.ViewModels.CalisanAvansViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "CalisanAvans", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n<div class=\"card border border-dark\">\r\n\r\n    <table class=\"table\" title=\"Avans Çizelgesi\">\r\n        <thead>\r\n            <tr>\r\n                <th");
            BeginWriteAttribute("class", " class=\"", 217, "\"", 225, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    ");
#nullable restore
#line 11 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
               Write(Html.DisplayNameFor(model => model.AvansTarihi));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th");
            BeginWriteAttribute("class", " class=\"", 341, "\"", 349, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    ");
#nullable restore
#line 14 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
               Write(Html.DisplayNameFor(model => model.AvansTalepEdilenTarih));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th");
            BeginWriteAttribute("class", " class=\"", 475, "\"", 483, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    ");
#nullable restore
#line 17 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
               Write(Html.DisplayNameFor(model => model.AvansOnaylandıgıTarih));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th");
            BeginWriteAttribute("class", " class=\"", 609, "\"", 617, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    ");
#nullable restore
#line 20 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
               Write(Html.DisplayNameFor(model => model.AvansVerildigiTarih));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th class=\"float-right\">\r\n                    ");
#nullable restore
#line 23 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
               Write(Html.DisplayNameFor(model => model.AvansMiktarı));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 29 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td");
            BeginWriteAttribute("class", " class=\"", 1016, "\"", 1024, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "18b240f7c2a3a12c0265db08cda43063f78ceb617499", async() => {
                WriteLiteral(" ");
#nullable restore
#line 33 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                                                                                                                                  Write(Html.DisplayFor(modelItem => item.AvansTarihi));

#line default
#line hidden
#nullable disable
                WriteLiteral("  ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 33 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                                                                                WriteLiteral(item.AvansTarihi.ToString("yyyy-MM-dd"));

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </td>\r\n                    <td>");
#nullable restore
#line 35 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                   Write(Html.DisplayFor(modelItem => item.AvansTalepEdilenTarih));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n");
#nullable restore
#line 37 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                           if (item.AvansOnaylandıgıTarih.Year < 1750)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <label>----</label>\r\n");
#nullable restore
#line 40 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                            }
                            else
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                           Write(Html.DisplayFor(modelItem => item.AvansOnaylandıgıTarih));

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                                                                                         
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </td>\r\n                    <td>\r\n");
#nullable restore
#line 48 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                           if (item.AvansVerildigiTarih.Year < 1750)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <label>----</label>\r\n");
#nullable restore
#line 51 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                            }
                            else
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                           Write(Html.DisplayFor(modelItem => item.AvansVerildigiTarih));

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                                                                                       
                            }
                        

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td class=\"float-right\">\r\n                        ");
#nullable restore
#line 60 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
                   Write(Html.DisplayFor(modelItem => item.AvansMiktarı));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                    <td>\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 65 "C:\Users\user\source\Workspaces\FinalProject\FinalProject\WebUI\Views\Shared\Components\CalisanAvanslar\Default.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DataAccess.ViewModels.CalisanAvansViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
