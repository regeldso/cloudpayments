namespace CloudPayments.Module.RazorViews
{
    #line hidden
    using System;
    using System.Threading.Tasks;
#line 1 "PaymentForm.cshtml"
using System.Globalization;

#line default
#line hidden
#line 2 "PaymentForm.cshtml"
using CloudPayments.Module;

#line default
#line hidden
    internal class PaymentForm : Microsoft.Extensions.RazorViews.BaseView
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 12 "PaymentForm.cshtml"
  
    Response.ContentType = "text/html; charset=utf-8";
    string location = string.Empty;

#line default
#line hidden
            WriteLiteral("<!DOCTYPE html>\r\n<html");
            BeginWriteAttribute("lang", " lang=\"", 335, "\"", 396, 1);
#line 17 "PaymentForm.cshtml"
WriteAttributeValue("", 342, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, 342, 54, false);

#line default
#line hidden
            EndWriteAttribute();
            WriteLiteral(@">
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
    <title>Оплата</title>
    <link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/css/bootstrap.min.css"" integrity=""sha384-rwoIResjU2yc3z8GV/NPeZWAv56rSmLldC3R/AZzGRnGxQQKnKkoFVhFQhNUwEyJ"" crossorigin=""anonymous"">
</head>
<body>
    <div class=""container"">
        <div class=""row"" style=""margin-top: 100px;"">
            <div class=""col-md-6 col-lg-4 col-sm-8 col-10 offset-md-3 offset-sm-2 offset-lg-4 offset-1"">
                <div class=""card"">
                    <div class=""card-header"">
                        ");
#line 30 "PaymentForm.cshtml"
                   Write(Model.PaymentFormHeader);

#line default
#line hidden
            WriteLiteral(@"
                    </div>
                    <div class=""card-block"">
                        <form id=""paymentForm"" autocomplete=""off"">
                            <div class=""form-group"">
                                <input type=""text"" class=""form-control"" data-cp=""cardNumber"" id=""cardNumber"" placeholder=""Номер карты"" />
                                <span class=""text-danger"" data-bind=""css: { 'msg-show': messages.cardNumber }, text: messages.cardNumber""></span>
                            </div>
                            <div class=""form-group"">
                                <input type=""text"" class=""form-control text-uppercase"" data-bind=""value: cardHolderName"" data-cp=""name"" id=""cardHolderName"" placeholder=""Имя латиницей"" />
                                <span class=""text-danger"" data-bind=""css: { 'msg-show': messages.name }, text: messages.name""></span>
                            </div>
                            <div class=""form-group"">
                                <div ");
            WriteLiteral(@"class=""row"">
                                    <div class=""form-group col-sm-4 col-lg-5 col-md-4 col-xl-4"">
                                        <input type=""text"" class=""form-control text-uppercase"" data-cp=""expDateMonthYear"" id=""cardExpDateMonthYear"" placeholder=""ММ/ГГ"" />
                                        <span class=""text-danger"" data-bind=""css: { 'msg-show': messages.expDateMonthYear }, text: messages.expDateMonthYear""></span>
                                    </div>
                                    <div class=""form-group col-sm-4 col-lg-5 col-md-4 col-xl-4"">
                                        <input type=""text"" class=""form-control text-uppercase"" data-cp=""cvv"" id=""cardCvv"" placeholder=""cvv"" />
                                        <span class=""text-danger"" data-bind=""css: { 'msg-show': messages.cvv }, text: messages.cvv""></span>
                                    </div>
                                </div>
                            </div>
                          ");
            WriteLiteral("  <button type=\"submit\" data-bind=\"click: createCryptogram\" class=\"btn btn-primary btn-block\">Оплатить ");
#line 54 "PaymentForm.cshtml"
                                                                                                                            Write(Model.Amount);

#line default
#line hidden
            WriteLiteral(" ");
#line 54 "PaymentForm.cshtml"
                                                                                                                                          Write(CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol);

#line default
#line hidden
            WriteLiteral(@"</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src=""https://code.jquery.com/jquery-3.2.1.min.js"" integrity=""sha256-hwg4gsxgFZhOsEEamdOYGBf13FyQuiTwlAQgxVSNgt4="" crossorigin=""anonymous""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.11/jquery.mask.min.js""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/tether/1.4.0/js/tether.min.js"" integrity=""sha384-DztdAPBWPRXSA/3eYEEUWrWCy7G5KFbe8fFjk5JAIxUYHKkDx6Qin1DkWx51bBrb"" crossorigin=""anonymous""></script>
    <script src=""https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/js/bootstrap.min.js"" integrity=""sha384-vBWWzlZJ8ea9aCX4pEW3rVHjgjt7zpkNpZk+02D9phzyeVkE+jo0ieGizqPLForn"" crossorigin=""anonymous""></script>
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/knockout/3.4.2/knockout-min.js""></script>
    <script src=""https://widget.cloudpayments.ru/bundles/checkout""></script>
    <script>
  ");
            WriteLiteral(@"       var checkout;

         function CartViewModel() {
             var viewModel = this;

             this.cardHolderName = ko.observable();

             this.messages = {
                 cardNumber: ko.observable(),
                 name: ko.observable(),
                 expDateMonthYear: ko.observable(),
                 cvv: ko.observable()
             };

             this.createCryptogram = function () {
                 var result = checkout.createCryptogramPacket();

                 if (result.success) {
                     var form = document.createElement(""form"");
                     form.action = """);
#line 87 "PaymentForm.cshtml"
                               Write(Model.Action);

#line default
#line hidden
            WriteLiteral("\";\r\n                     form.method = \"POST\";\r\n\r\n                     var cryptogram = document.createElement(\"input\");\r\n                     cryptogram.type = \"hidden\";\r\n                     cryptogram.name = \"");
#line 92 "PaymentForm.cshtml"
                                   Write(CloudPaymentsDefaults.FormKeyCryptogram);

#line default
#line hidden
            WriteLiteral("\";\r\n                     cryptogram.value = result.packet;\r\n\r\n                     var name = document.createElement(\"input\");\r\n                     name.type = \"hidden\";\r\n                     name.name = \"");
#line 97 "PaymentForm.cshtml"
                             Write(CloudPaymentsDefaults.FormKeyName);

#line default
#line hidden
            WriteLiteral("\";\r\n                     name.value = viewModel.cardHolderName();\r\n\r\n                     form.appendChild(cryptogram);\r\n                     form.appendChild(name);\r\n                     form.insertAdjacentHTML(\"beforeend\", \'");
#line 102 "PaymentForm.cshtml"
                                                      Write(Model.AntiforgeryToken);

#line default
#line hidden
            WriteLiteral(@"');
                     document.body.appendChild(form);
                     form.submit();
                 }
                 else {
                     // найдены ошибки в ведённых данных, объект `result.messages` формата:
                     // { name: ""В имени держателя карты слишком много символов"", cardNumber: ""Неправильный номер карты"" }
                     // где `name`, `cardNumber` соответствуют значениям атрибутов `<input ... data-cp=""cardNumber"">`
                     for (var msgName in viewModel.messages) {
                         viewModel.messages[msgName](result.messages[msgName]);
                     }
                 }
             };
         };

         var model = new CartViewModel();
         $(function () {

             $(""#cardNumber"").mask(""0000 0000 0000 0099 999"");
             $(""#cardCvv"").mask(""000"");
             $(""#cardExpDateMonthYear"").mask(""00/00"");

             /* Создание checkout */
             checkout = new cp.Checkout(
           ");
            WriteLiteral("      // ключ API\r\n                 \"");
#line 127 "PaymentForm.cshtml"
             Write(Model.PublicId);

#line default
#line hidden
            WriteLiteral(@""",
                 // тег, содержащий теги с данными карты (<form id=""paymentForm"">)
                 document.getElementById(""paymentForm""));

             ko.applyBindings(model, document.getElementById(""mainContent"")); // onready - чтобы вызов фрейма не произошёл до загрузки DOM.
         });
    </script>
</body>
</html>");
        }
        #pragma warning restore 1998
#line 4 "PaymentForm.cshtml"
 
    public PaymentForm(PaymentFormModel model)
    {
        Model = model;
    }

    public PaymentFormModel Model { get; set; }

#line default
#line hidden
    }
}
