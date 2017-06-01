// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using Microsoft.Extensions.Options;

namespace CloudPayments.Client
{
    public class CloudPaymentsClientOptions : IOptions<CloudPaymentsClientOptions>
    {
        public string PublicId { get; set; }
        public string ApiKey { get; set; }

        public CloudPaymentsClientOptions Value => this;
    }
}
