// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using CloudPayments.Client;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CloudPayments.Module
{
    public abstract class CloudPaymentsMiddlewareBase
    {
        protected readonly RequestDelegate _next;
        protected readonly CloudPaymentsOptions _options;
        protected readonly ILogger _logger;
        protected readonly ICloudPaymentsClient _client;
        protected readonly ITempDataDictionaryFactory _tempDataFactory;
        protected readonly IAntiforgery _antiforgery;

        protected CloudPaymentsMiddlewareBase(
            RequestDelegate next,
            IOptions<CloudPaymentsOptions> options,
            ILoggerFactory loggerFactory,
            ITempDataDictionaryFactory tempDataFactory,
            ICloudPaymentsClient client,
            IAntiforgery antiforgery)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options.Value;
            _logger = loggerFactory.CreateLogger<CloudPaymentsMiddleware>();
            _tempDataFactory = tempDataFactory ?? throw new ArgumentNullException(nameof(tempDataFactory));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _antiforgery = antiforgery ?? throw new ArgumentNullException(nameof(antiforgery));
        }

        public abstract Task Invoke(HttpContext context);
    }
}
