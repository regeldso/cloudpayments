// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
namespace CloudPayments.Client
{
    public class Secure3DRequest
    {
        public int TransactionId { get; set; }

        public string PaRes { get; set; }
    }
}
