#pragma checksum "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Blog\BlogDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "366a139d116f2592d33a87e3073f23c37f7f2dfa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Blog_BlogDetail), @"mvc.1.0.view", @"/Views/Blog/BlogDetail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"366a139d116f2592d33a87e3073f23c37f7f2dfa", @"/Views/Blog/BlogDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"092c3731ce63fea3123d24226832cf1e626c21e2", @"/Views/_ViewImports.cshtml")]
    public class Views_Blog_BlogDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Blog>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:100%"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-fluid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"<div class=""container"">
    <div class=""row"">
        <div class=""col-lg-12 col-md-12 col-sm-12 col-xs-12"">
            <div class=""page-wrapper"">
                <div class=""blog-custom-build"">
                    
                        <div class=""blog-box wow fadeIn"">
                            <div class=""post-media"">
                                <a href=""marketing-single.html""");
            BeginWriteAttribute("title", " title=\"", 411, "\"", 419, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "366a139d116f2592d33a87e3073f23c37f7f2dfa4932", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 488, "~/assets/images/blogs/", 488, 22, true);
#nullable restore
#line 11 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Blog\BlogDetail.cshtml"
AddHtmlAttributeValue("", 510, Model.Image, 510, 12, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                    <div class=""hovereffect"">
                                        <span></span>
                                    </div>
                                    <!-- end hover -->
                                </a>
                            </div>
                            <!-- end media -->
                            <div class=""blog-meta big-meta text-center"">
                                <div class=""post-sharing"">

                                </div><!-- end post-sharing -->
                                <h4><a");
            BeginWriteAttribute("href", " href=\"", 1130, "\"", 1156, 1);
#nullable restore
#line 23 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Blog\BlogDetail.cshtml"
WriteAttributeValue("", 1137, Model.RedirectUrll, 1137, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("title", " title=\"", 1157, "\"", 1165, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 23 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Blog\BlogDetail.cshtml"
                                                                      Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h4>\r\n                                <p>");
#nullable restore
#line 24 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Blog\BlogDetail.cshtml"
                              Write(Model.Desc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                <small><a href=\"marketing-single.html\"");
            BeginWriteAttribute("title", " title=\"", 1312, "\"", 1320, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 25 "C:\Users\user\Desktop\MyFinalProject\MyFinalProject\Views\Blog\BlogDetail.cshtml"
                                                                           Write(Model.Date.ToString("dd-MM-yyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</a></small>
                            </div><!-- end meta -->
                        </div><!-- end blog-box -->

                        <hr class=""invis"">
                    
                </div><!-- end blog-box -->
            </div>
        </div>
    </div>
</div>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Blog> Html { get; private set; }
    }
}
#pragma warning restore 1591
