#pragma checksum "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e552f458a9dbbf304cf7603b00fa5153384c9aaa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Group_ViewGroup), @"mvc.1.0.view", @"/Views/Group/ViewGroup.cshtml")]
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
#line 1 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\_ViewImports.cshtml"
using StudyGroup;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\_ViewImports.cshtml"
using StudyGroup.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e552f458a9dbbf304cf7603b00fa5153384c9aaa", @"/Views/Group/ViewGroup.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"20a75bc9aaf8afb41446876bcee6ebcc281a3752", @"/Views/_ViewImports.cshtml")]
    public class Views_Group_ViewGroup : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<StudyGroup.ViewModels.ViewAGroupViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_GroupNav", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_GroupDetails", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Group", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Leave", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax", new global::Microsoft.AspNetCore.Html.HtmlString("true"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-method", new global::Microsoft.AspNetCore.Html.HtmlString("post"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-complete", new global::Microsoft.AspNetCore.Html.HtmlString("completed"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-dismiss", new global::Microsoft.AspNetCore.Html.HtmlString("modal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Lock", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
  
    ViewData["Title"] = "ViewGroup";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"row\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e552f458a9dbbf304cf7603b00fa5153384c9aaa7184", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    <div class=\"col-md-9\">\r\n\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e552f458a9dbbf304cf7603b00fa5153384c9aaa8344", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            WriteLiteral("\r\n\r\n        <hr />\r\n        <div class=\"text-right mb-2\">\r\n");
#nullable restore
#line 19 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
             if (Model.CurrentUser == Model.Group.GroupAdmin)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <button class=""btn btn-outline-danger"" data-toggle=""modal"" data-target=""#Delete""> <i class=""fas fa-trash-alt""> Delete</i> </button>
                <button class=""btn btn-outline-danger"" data-toggle=""modal"" data-target=""#lockUnlock"" onclick=""Hey()"">
");
#nullable restore
#line 23 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                     if (Model.Group.isClosed == true)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<i id=\"block\" class=\"fas fa-lock-open\"> Unlock</i>");
#nullable restore
#line 24 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                                                                       }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<i id=\"block\" class=\"fas fa-lock\"> Lock</i>");
#nullable restore
#line 26 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </button>\r\n");
#nullable restore
#line 28 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <button class=\"btn btn-outline-danger\" data-toggle=\"modal\" data-target=\"#Leave\"><i class=\"fas fa-sign-out-alt\">Leave</i></button>\r\n");
#nullable restore
#line 32 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </div>

        <div class=""modal fade"" id=""Leave"" data-backdrop=""static"" tabindex=""-1"" role=""dialog"">
            <div class=""modal-dialog modal-dialog-centered"" role=""document"">
                <div class=""modal-content"">
                    <div class=""modal-header text-center"">
                        <h5 class=""modal-title"">Are You Sure You want to Leave this Group?</h5>
                        <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                            <span aria-hidden=""true"">&times;</span>
                        </button>
                    </div>
                    <div class=""modal-body text-center"">
                        If you Leave this Group, All the files you sent will still remain available to this group
                    </div>
                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Don't!</button>
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e552f458a9dbbf304cf7603b00fa5153384c9aaa12828", async() => {
                WriteLiteral("Leave Group");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-name", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 49 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                                                                                                WriteLiteral(Model.Group.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["name"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-name", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["name"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </div>
                </div>
            </div>
        </div>

        <div class=""modal fade"" id=""lockUnlock"" data-backdrop=""static"" tabindex=""-1"" role=""dialog"">
            <div class=""modal-dialog modal-dialog-centered"" role=""document"">
                <div class=""modal-content"">
                    <div class=""modal-header"">
                        <h5 class=""modal-title text-center"">
                            Are You Sure You want to
");
#nullable restore
#line 61 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                             if (Model.Group.isClosed == true)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("<span id=\"editlock\">Unlock this group</span>");
#nullable restore
#line 62 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                                                                         }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("<span id=\"editlock\">Lock this group? If you Lock this Group, No new Member will be able to Join it</span>");
#nullable restore
#line 64 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                                                                                                                                      }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </h5>
                        <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                            <span aria-hidden=""true"">&times;</span>
                        </button>
                    </div>

                    <div class=""modal-footer"">
                        <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">Don't!</button>
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e552f458a9dbbf304cf7603b00fa5153384c9aaa17467", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 75 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                             if (Model.Group.isClosed == true)
                            {

#line default
#line hidden
#nullable disable
                WriteLiteral("<span id=\"editlock2\">Unlock</span> ");
#nullable restore
#line 76 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                                                                }
                            else
                            {

#line default
#line hidden
#nullable disable
                WriteLiteral("<span id=\"editlock2\">Lock</span>");
#nullable restore
#line 78 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                                                             }

#line default
#line hidden
#nullable disable
                WriteLiteral("                            Group\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-name", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 74 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                                                                                                                                                                                            WriteLiteral(Model.Group.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["name"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-name", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["name"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                    </div>
                </div>
            </div>
        </div>

        <div class=""modal fade"" id=""Delete"" data-backdrop=""static"" tabindex=""-1"" role=""dialog"">
            <div class=""modal-dialog modal-dialog-centered"" role=""document"">
                <div class=""modal-content"">
                    <div class=""modal-header"">
                        <h5 class=""modal-title"">Are You Sure You want to Delete this Group?</h5>
                        <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                            <span aria-hidden=""true"">&times;</span>
                        </button>
                    </div>
                    <div class=""modal-body"">
                        If you Delete this Group, All You Unsent Reminders will be terminated. Your documents will
                        be deleted. All Group members will be logged out and this group will not exist anymore
                    </div>
                    <div class");
            WriteLiteral("=\"modal-footer\">\r\n                        <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Don\'t Delete</button>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e552f458a9dbbf304cf7603b00fa5153384c9aaa22720", async() => {
                WriteLiteral("Delete Group");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-name", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 101 "C:\Users\hp\Documents\COFFEE BEAN\PROJECT\StudyGroup\StudyGroup\Views\Group\ViewGroup.cshtml"
                                                                                                 WriteLiteral(Model.Group.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["name"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-name", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["name"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"

        <script>
            var completed = function () {
                if (document.getElementById(""block"").className == ""fas fa-lock-open"")
                {
                    document.getElementById(""block"").className = ""fas fa-lock""
                    document.getElementById(""block"").innerHTML = ""Lock""
                }
                else {
                    document.getElementById(""block"").className = ""fas fa-lock-open""
                    document.getElementById(""block"").innerHTML = ""Unlock""
                } 
            }

            function Hey() {
                if (document.getElementById(""block"").className == ""fas fa-lock-open"") {
                    document.getElementById(""editlock"").innerHTML = ""Unlock this group?""
                    document.getElementById(""editlock2"").textContent = ""Unlock""

                }
                else {
                    document.getElementById(""editlock"").innerHTML = ""Lock this group? If you Lock this Group, No new Member w");
                WriteLiteral("ill be able to Join it\"\r\n                    document.getElementById(\"editlock2\").textContent = \"Lock\"\r\n\r\n                } \r\n            }\r\n        </script>\r\n    ");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<StudyGroup.ViewModels.ViewAGroupViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
