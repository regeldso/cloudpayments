// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
namespace CloudPayments.Client
{
    static class CloudPaymentsClientDefaults
    {
        public static readonly string TestEndpoint = "https://api.cloudpayments.ru/test";

        public static readonly string PaymentCryptogramSingleStageEndpoint = "https://api.cloudpayments.ru/payments/cards/charge";

        public static readonly string PaymentCryptogramTwoStageEndpoint = "https://api.cloudpayments.ru/payments/cards/auth";

        public static readonly string Secure3DEndpoint = "https://api.cloudpayments.ru/payments/cards/post3ds";
    }
}
