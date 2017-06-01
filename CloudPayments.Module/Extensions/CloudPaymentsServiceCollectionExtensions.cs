// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using CloudPayments.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace CloudPayments.Module
{
    public static class CloudPaymentsServiceCollectionExtensions
    {
        public static IServiceCollection AddCloudPayments(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();
            services.TryAddSingleton<ICloudPaymentsClient, CloudPaymentsClient>();

            return services;
        }

        public static IServiceCollection AddCloudPayments(
            this IServiceCollection services,
            Action<CloudPaymentsClientOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            services.AddCloudPayments();
            services.Configure(options);

            return services;
        }
    }
}
