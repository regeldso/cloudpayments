// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using Microsoft.Extensions.Options;

namespace CloudPayments.Module
{
    public class CloudPaymentsOptions : IOptions<CloudPaymentsOptions>
    {
        public string ReturnUrl { get; set; }

        public CloudPaymentsOptions Value => this;
    }
}
