#pragma checksum "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a9d52025d8d99e3efff69ec652a76329933a9021"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
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
#line 1 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\_ViewImports.cshtml"
using MyFinalProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\_ViewImports.cshtml"
using MyFinalProject.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\_ViewImports.cshtml"
using MyFinalProject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9d52025d8d99e3efff69ec652a76329933a9021", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"092c3731ce63fea3123d24226832cf1e626c21e2", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AboutViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-img-top"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Card image cap"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"    <main style=""padding: 50px;"">
        <section id=""about"">
            <div class=""container"">
                <div class=""col-lg-12"">
                    <div style=""background-color: white; text-align: center; padding: 30px 0;"" class=""content"">
                        <div style=""padding: 30px;"" class=""title"">
                            <div class=""h2"">");
#nullable restore
#line 8 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Home\About.cshtml"
                                       Write(Model.Settings["aboutTitle"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                        </div>\r\n                        <div style=\"padding: 20px 70px; font-size: 20px;\" class=\"text\">\r\n                            <p>\r\n                                ");
#nullable restore
#line 12 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Home\About.cshtml"
                           Write(Model.Settings["aboutDesc1"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </p>\r\n                            <p>\r\n                                ");
#nullable restore
#line 15 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Home\About.cshtml"
                           Write(Model.Settings["aboutDesc2"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section id=""team"">
            <div class=""container"">
                <div style=""text-align: center;"" class=""team-content "">
                    <div class=""team-title"">
                        <h1>Meet Our Team</h1>
                    </div>
                    <div class=""sub-title"">
                        <p style=""margin: 0;"">Lorem ipsum dolor sit.</p>
                        <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit.</p>
                    </div>
                </div>
                <div style=""display: flex;"" class=""team-members "">
");
#nullable restore
#line 34 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Home\About.cshtml"
                     foreach (var team in Model.OurTeams)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"col-lg-4\">\r\n                        <div class=\"card\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "a9d52025d8d99e3efff69ec652a76329933a90216621", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1752, "~/upload/teams/", 1752, 15, true);
#nullable restore
#line 38 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Home\About.cshtml"
AddHtmlAttributeValue("", 1767, team.Image, 1767, 11, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            <div class=\"card-body\">\r\n                                <h5 class=\"card-title\">");
#nullable restore
#line 40 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Home\About.cshtml"
                                                  Write(team.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                <p class=\"card-text\">");
#nullable restore
#line 41 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Home\About.cshtml"
                                                Write(team.Position);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 45 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Home\About.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n        </section>\r\n    </main>\r\n   ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AboutViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591