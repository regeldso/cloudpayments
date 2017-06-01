// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CloudPayments.Client
{
    public class PaymentTokenRequest
    {
        public decimal Amount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CurrencyCodes Currency { get; set; }

        public string AccountId { get; set; }

        public string Token { get; set; }

        public string InvoiceId { get; set; }

        public string Description { get; set; }

        public string IPAddress { get; set; }

        public string Email { get; set; }

        public string JsonData { get; set; }
    }
}
