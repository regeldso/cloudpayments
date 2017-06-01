// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace CloudPayments.Module
{
    public static class CloudPaymentsExtensions
    {
        public static IApplicationBuilder UseCloudPayments(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.MapWhen(context => context.Request.Path == CloudPaymentsDefaults.PayPath,
                app => app.UseMiddleware<CloudPaymentsMiddleware>());

            builder.MapWhen(context => context.Request.Path == CloudPaymentsDefaults.Secure3dPath,
                app => app.UseMiddleware<CloudPaymentsMiddleware3dSecure>());
            return builder;
        }

        public static IApplicationBuilder UseCloudPayments(
            this IApplicationBuilder builder,
            CloudPaymentsOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var iOptions = Options.Create(options);

            builder.MapWhen(context => context.Request.Path == CloudPaymentsDefaults.PayPath,
               app => app.UseMiddleware<CloudPaymentsMiddleware>(iOptions));

            builder.MapWhen(context => context.Request.Path == CloudPaymentsDefaults.Secure3dPath,
                app => app.UseMiddleware<CloudPaymentsMiddleware3dSecure>(iOptions));

            return builder;
        }

        internal static string BuildAbsolute(this HttpRequest request, string path) => 
            UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, path);
    }
}
