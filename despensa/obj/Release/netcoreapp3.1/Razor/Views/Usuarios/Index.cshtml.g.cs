#pragma checksum "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "27409787046e4eb58601f42454f4985c7ccd94dc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Usuarios_Index), @"mvc.1.0.view", @"/Views/Usuarios/Index.cshtml")]
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
#line 1 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\_ViewImports.cshtml"
using despensa;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\_ViewImports.cshtml"
using despensa.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27409787046e4eb58601f42454f4985c7ccd94dc", @"/Views/Usuarios/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f631de54767318e1d6dbe9f06728805a3ae1783", @"/Views/_ViewImports.cshtml")]
    public class Views_Usuarios_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<despensa.Models.Usuario>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/login.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("botones info redondear"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
  
    ViewData["Title"] = "Usuario";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
    #card {
        height: 250px;
        width: 300px;
        margin: 0 auto;
        position: relative;
        z-index: 1;
        perspective: 600px;
    }

        #card #front {
            border-radius: 10px;
            height: 100%;
            width: 100%;
            position: absolute;
            left: 0;
            top: 0;
            transform-style: preserve-3d;
            backface-visibility: hidden;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
            transform: rotateY(0deg);
            overflow: hidden;
            z-index: 1;
        }

            #card #front #top-pic {
                height: 50%;
                width: 100%;
                background-color: #1accfd;
                background-size: cover;
            }

            #card #front #avatar {
                width: 114px;
                height: 114px;
                top: 50%;
                left: 50%;
                margin: -77px 0 0 -57px;
             ");
            WriteLiteral(@"   border-radius: 100%;
                box-shadow: 0 0 0 3px rgba(255, 255, 255, 0.8), 0 4px 5px rgba(107, 5, 0, 0.6), 0 0 50px 50px rgba(255, 255, 255, 0.25);
                background-size: cover;
                position: absolute;
                z-index: 1;
            }

            #card #front #info-box {
                height: 125px;
                width: 100%;
                position: absolute;
                display: table;
                left: 0;
                bottom: 0;
                background: rgba(255, 87, 29, 0.7);
            }

            #card #front a {
                display: inline-block;
                color: #951009;
                text-decoration: none;
                padding: 5px;
                line-height: 18px;
                border-radius: 5px;
            }

                #card #front a:hover {
                    color: #450300;
                    background: rgba(255, 255, 255, 0.3);
                    transition: .25s ease-i");
            WriteLiteral("n-out;\r\n                }\r\n\r\n        #card .info {\r\n            display: table-cell;\r\n            height: 100%;\r\n            vertical-align: middle;\r\n            text-align: center;\r\n        }\r\n</style>\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "27409787046e4eb58601f42454f4985c7ccd94dc8663", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<div class=""ads-grid"" style=""padding-top:0px;"">
    <div class=""container"" style=""width: 90%;"">
        <!-- tittle heading -->
        <div class=""container"">
            <h3 class=""tittle-w3l"">
                Usuarios
                <span class=""heading-style"">
                    <i></i>
                    <i></i>
                    <i></i>
                </span>
            </h3>
        </div>
        
        <div class=""container"" style=""margin-bottom: 10px;"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "27409787046e4eb58601f42454f4985c7ccd94dc10299", async() => {
                WriteLiteral(@"
                <div class=""col-6 col-md-3"">
                    <input id=""Nombre"" name=""Nombre"" type=""text"" placeholder=""Nombre"" />
                </div>
                <div class=""col-6 col-md-3"">
                    <input id=""Apellido"" name=""Apellido"" type=""text"" placeholder=""Apellido"" />
                </div>
                <div class=""col-6 col-md-3"">
                    <input id=""cui"" name=""cui"" type=""text"" placeholder=""DPI"" />
                </div>
                <input type=""submit"" value=""Filtrar"" style=""max-width: 10%;"" class=""botones success redondear"">
                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "27409787046e4eb58601f42454f4985c7ccd94dc11201", async() => {
                    WriteLiteral("Crear Usuario");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n        <div class=\"agileinfo-ads-display col-md-16\">\r\n            <div class=\"wrapper\">\r\n                <div class=\"product-sec1\">\r\n");
#nullable restore
#line 117 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        <div class=""col-md-4  product-men"">
                            <div id=""card"" style=""width: 99%;"">
                                <div id=""front"">
                                    <div id=""top-pic""></div>
                                    <div id=""avatar""");
            BeginWriteAttribute("style", " style=\"", 4194, "\"", 4285, 5);
            WriteAttributeValue("", 4202, "background-image:", 4202, 17, true);
            WriteAttributeValue(" ", 4219, "url(\'", 4220, 6, true);
#nullable restore
#line 123 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
WriteAttributeValue("", 4225, Url.Content("~/perfiles/" + item.ImagenPerfil), 4225, 47, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4272, "\');", 4272, 3, true);
            WriteAttributeValue(" ", 4275, "top:80px;", 4276, 10, true);
            EndWriteAttribute();
            WriteLiteral("></div>\r\n                                    <div id=\"info-box\">\r\n                                        <div class=\"info\">\r\n                                            <h3> ");
#nullable restore
#line 126 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
                                            Write(Html.DisplayFor(modelItem => item.PrimerNombre));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 126 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
                                                                                             Write(Html.DisplayFor(modelItem => item.PrimerApellido));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                                            <h4>");
#nullable restore
#line 127 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
                                           Write(Html.DisplayFor(modelItem => item.CodRolNavigation.Rol1));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                                            <h4>");
#nullable restore
#line 128 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
                                           Write(Html.DisplayFor(modelItem => item.CodEstadoNavigation.Estado));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "27409787046e4eb58601f42454f4985c7ccd94dc16891", async() => {
                WriteLiteral("Ver Detalles");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 129 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
                                                                   WriteLiteral(item.CodUsuario);

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
            WriteLiteral("\r\n                                        </div>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 135 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"clearfix\"></div>\r\n                </div>\r\n            </div>\r\n\r\n");
#nullable restore
#line 140 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
             if (ViewBag.cui != null || ViewBag.cui != "" || ViewBag.apellido != null || ViewBag.apellido != "" || ViewBag.nombres != null || ViewBag.nombres != "")
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 142 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
           Write(Html.PagedListPager((IPagedList)Model, page => Url.Action("Index", new { page = page, Nombre = @ViewBag.nombres, Apellido = @ViewBag.apellido, cui= @ViewBag.cui })));

#line default
#line hidden
#nullable disable
#nullable restore
#line 142 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
                                                                                                                                                                                     
            }
            else
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 146 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
           Write(Html.PagedListPager((IPagedList)Model, page => Url.Action("Index", new { page = page})));

#line default
#line hidden
#nullable disable
#nullable restore
#line 146 "D:\proyectosvisual\Despensa-Virtual1\despensa\Views\Usuarios\Index.cshtml"
                                                                                                        
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<despensa.Models.Usuario>> Html { get; private set; }
    }
}
#pragma warning restore 1591
