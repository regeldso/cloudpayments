namespace CloudPayments.Module.RazorViews
{
    #line hidden
    using System;
    using System.Threading.Tasks;
    internal class Secure3dForm : Microsoft.Extensions.RazorViews.BaseView
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 10 "Secure3dForm.cshtml"
  
    Response.ContentType = "text/html; charset=utf-8";
    string location = string.Empty;

#line default
#line hidden
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n</head>\r\n<body>\r\n    <form name=\"downloadForm\"");
            BeginWriteAttribute("action", " action=\"", 369, "\"", 391, 1);
#line 21 "Secure3dForm.cshtml"
WriteAttributeValue("", 378, Model.AcsUrl, 378, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            WriteLiteral(" method=\"POST\">\r\n        <input type=\"hidden\" name=\"PaReq\"");
            BeginWriteAttribute("value", " value=\"", 450, "\"", 470, 1);
#line 22 "Secure3dForm.cshtml"
WriteAttributeValue("", 458, Model.PaReq, 458, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            WriteLiteral(">\r\n        <input type=\"hidden\" name=\"MD\"");
            BeginWriteAttribute("value", " value=\"", 512, "\"", 529, 1);
#line 23 "Secure3dForm.cshtml"
WriteAttributeValue("", 520, Model.MD, 520, 9, false);

#line default
#line hidden
            EndWriteAttribute();
            WriteLiteral(">\r\n        <input type=\"hidden\" name=\"TermUrl\"");
            BeginWriteAttribute("value", " value=\"", 576, "\"", 598, 1);
#line 24 "Secure3dForm.cshtml"
WriteAttributeValue("", 584, Model.TempUrl, 584, 14, false);

#line default
#line hidden
            EndWriteAttribute();
            WriteLiteral(">\r\n    </form>\r\n    <script>\r\n        window.onload = submitForm;\r\n        function submitForm() { downloadForm.submit(); }\r\n    </script>\r\n</body>\r\n</html>\r\n");
        }
        #pragma warning restore 1998
#line 2 "Secure3dForm.cshtml"
 
    public Secure3dForm(Secure3dFormModel model)
    {
        Model = model;
    }

    public Secure3dFormModel Model { get; set; }

#line default
#line hidden
    }
}
