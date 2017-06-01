// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using Microsoft.AspNetCore.Html;

namespace CloudPayments.Module.RazorViews
{
    internal class PaymentFormModel
    {
        public string PublicId { get; set; }

        public decimal Amount { get; set; }

        public string Action { get; set; }

        public string PaymentFormHeader { get; set; } = "Оплата";

        public IHtmlContent AntiforgeryToken { get; set; }
    }
}
