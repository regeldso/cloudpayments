// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System;

namespace CloudPayments.Client
{
    public class Model
    {
        public int TransactionId { get; set; }

        public decimal Amount { get; set; }

        public CurrencyCodes Currency { get; set; }

        public decimal PaymentAmount { get; set; }

        public CurrencyCodes PaymentCurrency { get; set; }

        public string InvoiceId { get; set; }

        public string AccountId { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string JsonData { get; set; }

        public DateTime CreateDate { get; set; }

        public bool TestMode { get; set; }

        public string IPAddress { get; set; }

        public string IPCountry { get; set; }

        public string IPCity { get; set; }

        public string IPRegion { get; set; }

        public string IPDistrict { get; set; }

        public double IPLatitude { get; set; }

        public double IPLongitude { get; set; }

        public string CardFirstSix { get; set; }

        public string CardLastFour { get; set; }

        public DateTime CardExpDate { get; set; }

        public CardTypes CardType { get; set; }

        public string Issuer { get; set; }

        public string IssuerBankCountry { get; set; }

        public TransactionStatusCodes Status { get; set; }

        public ReasonCodes Reason { get; set; }

        public string CardHolderMessage { get; set; }

        public string Name { get; set; }

        public string PaReq { get; set; }

        public string AcsUrl { get; set; }
    }
}
