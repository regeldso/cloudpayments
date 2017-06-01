// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
namespace CloudPayments.Client
{
    public class PaymentResponse
    {
        public Model Model { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public bool IsSecure3D => !(string.IsNullOrWhiteSpace(Model?.AcsUrl) || string.IsNullOrWhiteSpace(Model?.PaReq));
    }
}
