#pragma checksum "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0b333895e618aeaa43f5bb86a3bdf2577d48dc0e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PredidoFacturas_Create), @"mvc.1.0.view", @"/Views/PredidoFacturas/Create.cshtml")]
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
#line 1 "D:\proyectosvisual\despensa2\despensa\despensa\Views\_ViewImports.cshtml"
using despensa;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\proyectosvisual\despensa2\despensa\despensa\Views\_ViewImports.cshtml"
using despensa.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0b333895e618aeaa43f5bb86a3bdf2577d48dc0e", @"/Views/PredidoFacturas/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f631de54767318e1d6dbe9f06728805a3ae1783", @"/Views/_ViewImports.cshtml")]
    public class Views_PredidoFacturas_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<despensa.Models.PredidoFactura>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml"
  
    ViewData["Title"] = "Create";
    Layout = "~/Views/Shared/vistasincarro.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""privacy"">
    <div class=""container"">
        <!-- tittle heading -->
        <h3 class=""tittle-w3l"">
            Mi Pedido


            <span class=""heading-style"">
                <i></i>
                <i></i>
                <i></i>
            </span>
        </h3>
");
#nullable restore
#line 20 "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml"
         if (@ViewBag.ErrorCarrito == 1)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-danger text-center\">\r\n                <strong>Su Compra Debe Ser Mayor A Q50</strong>\r\n            </div>\r\n");
#nullable restore
#line 25 "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml"

        }
        else if (@ViewBag.ErrorCarrito == 2)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-danger text-center\">\r\n                <strong>En Estos Momentos No Contamos Las Unidades Suficientes De:</strong>\r\n                <strong>");
#nullable restore
#line 31 "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml"
                   Write(ViewBag.Inexistencia.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral(" , Quedan ");
#nullable restore
#line 31 "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml"
                                                         Write(ViewBag.Inexistencia.Cantidad);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Unidades</strong>\r\n            </div>\r\n");
#nullable restore
#line 33 "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml"

        }
        else if (@ViewBag.ErrorCarrito == 3)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert alert-danger text-center\">\r\n                <strong>El Carrito Se Encuentra Vacio</strong>\r\n            </div>\r\n");
#nullable restore
#line 40 "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <!-- //tittle heading -->\r\n        <div class=\"checkout-right\">\r\n            <h4>\r\n                Usted ha añadido:\r\n                <span id=\"cantidad_producntos\">0 Productos ");
#nullable restore
#line 45 "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml"
                                                      Write(ViewBag.ErrorCarrito);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>
            </h4>
            <div class=""table-responsive"">
                <table id=""lstProductos"" class=""timetable_sub"">
                    <thead>
                        <tr>
                            <th>No.</th>
                            <th>Producto</th>
                            <th>Cantidad</th>
                            <th>Descripción</th>
                            <th>Precio</th>
                            <th>Remover</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan=""4"">Total</td>
                            <td class=""total""></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        <div class=""checkout-left"">
            <div class=""address_form_agile"">
                <h4>Corrobore Sus Datos</h4>
           ");
            WriteLiteral("     ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0b333895e618aeaa43f5bb86a3bdf2577d48dc0e8281", async() => {
                WriteLiteral("\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0b333895e618aeaa43f5bb86a3bdf2577d48dc0e8559", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 74 "D:\proyectosvisual\despensa2\despensa\despensa\Views\PredidoFacturas\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    <div class=\"checkout-right-basket\">\r\n                        <input type=\"submit\" value=\"Realizar Pedido\" class=\"btn btn-default\" />\r\n                    </div>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n            <div class=\"clearfix\"> </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<despensa.Models.PredidoFactura> Html { get; private set; }
    }
}
#pragma warning restore 1591
