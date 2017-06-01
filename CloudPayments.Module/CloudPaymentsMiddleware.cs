// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using CloudPayments.Module.RazorViews;
using CloudPayments.Client;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using System;

namespace CloudPayments.Module
{
    internal class CloudPaymentsMiddleware : CloudPaymentsMiddlewareBase
    {
        private CloudPaymentsClientOptions _clientOptions;

        public CloudPaymentsMiddleware(
          RequestDelegate next,
          IOptions<CloudPaymentsOptions> options,
          IOptions<CloudPaymentsClientOptions> clientOptions,
          ILoggerFactory loggerFactory,
          ITempDataDictionaryFactory tempDataFactory,
          ICloudPaymentsClient client,
          IAntiforgery antiforgery) : base(next, options, loggerFactory, tempDataFactory, client, antiforgery)
        {
            if (clientOptions == null)
            {
                throw new ArgumentNullException(nameof(clientOptions));
            }

            _clientOptions = clientOptions.Value;
        }

        public override async Task Invoke(HttpContext context)
        {
            if (HttpMethods.IsGet(context.Request.Method))
            {
                var tempData = _tempDataFactory.GetTempData(context);
                var requestTempData = tempData.Get<Payment>(CloudPaymentsDefaults.TempDataKey);
                if (requestTempData == null)
                {
                    _logger.EmptyTempData(CloudPaymentsDefaults.TempDataKey);
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                tempData.Keep(CloudPaymentsDefaults.TempDataKey);
                tempData.Save();

                await new PaymentForm(new PaymentFormModel
                {
                    Amount = requestTempData.Amount,
                    PublicId = _clientOptions.PublicId,
                    AntiforgeryToken = _antiforgery.GetHtml(context),
                    Action = context.Request.GetEncodedUrl(),
                    PaymentFormHeader = requestTempData.Description
                }).ExecuteAsync(context);
                return;
            }

            if (HttpMethods.IsPost(context.Request.Method))
            {
                var valid = await _antiforgery.IsRequestValidAsync(context);
                if (!valid)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                if (!context.Request.HasFormContentType)
                {
                    _logger.NotFormContentType();
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                var form = await context.Request.ReadFormAsync();
                if (!(form.TryGetValue(CloudPaymentsDefaults.FormKeyName, out var name)
                    && form.TryGetValue(CloudPaymentsDefaults.FormKeyCryptogram, out var cryptogram)))
                {
                    _logger.MissingRequiredFormKeys(CloudPaymentsDefaults.FormKeyName, CloudPaymentsDefaults.FormKeyCryptogram);
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                var requestTempData = _tempDataFactory.GetTempData(context).Get<Payment>(CloudPaymentsDefaults.TempDataKey);
                var response = await _client.PayCryptogram(new PaymentCryptogramRequest
                {
                    Name = name.ToString().ToUpperInvariant(),
                    CardCryptogramPacket = cryptogram.ToString(),
                    Amount = requestTempData.Amount,
                    Currency = requestTempData.Currency,
                    AccountId = requestTempData.AccountId,
                    Description = requestTempData.Description,
                    Email = requestTempData.Email,
                    InvoiceId = requestTempData.InvoiceId,
                    JsonData = requestTempData.JsonData,
                    IPAddress = context.Connection.RemoteIpAddress.ToString()
                }, requestTempData.TwoStage);

                if (response.IsSecure3D)
                {
                    _tempDataFactory.GetTempData(context).Save(CloudPaymentsDefaults.TempDataKey, response);
                    var secure3dLocation = context.Request.BuildAbsolute(CloudPaymentsDefaults.Secure3dPath);
                    _logger.LocalRedirectExecuting(secure3dLocation);
                    context.Response.Redirect(secure3dLocation);
                    return;
                }

                if (response.Success)
                {
                    _logger.PaymentSuccess();
                }
                else
                {
                    _logger.PaymentFailed(response.Message);
                }

                _tempDataFactory.GetTempData(context).Save(CloudPaymentsDefaults.TempDataKey, response);
                var location = context.Request.BuildAbsolute(_options.ReturnUrl);
                _logger.LocalRedirectExecuting(location);
                context.Response.Redirect(location);
                return;
            }

            await _next(context);
        }
    }
}
