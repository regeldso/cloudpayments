// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using CloudPayments.Module.RazorViews;
using CloudPayments.Client;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CloudPayments.Module
{
    internal class CloudPaymentsMiddleware3dSecure : CloudPaymentsMiddlewareBase
    {
        public CloudPaymentsMiddleware3dSecure(
         RequestDelegate next,
         IOptions<CloudPaymentsOptions> options,
         ILoggerFactory loggerFactory,
         ITempDataDictionaryFactory tempDataFactory,
         ICloudPaymentsClient client,
         IAntiforgery antiforgery) : base(next, options, loggerFactory, tempDataFactory, client, antiforgery) { }

        public override async Task Invoke(HttpContext context)
        {
            if (HttpMethods.IsGet(context.Request.Method))
            {
                var responseTempData = _tempDataFactory.GetTempData(context).Get<PaymentResponse>(CloudPaymentsDefaults.TempDataKey);
                if (responseTempData == null)
                {
                    _logger.EmptyTempData(CloudPaymentsDefaults.TempDataKey);
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                await new Secure3dForm(new Secure3dFormModel
                {
                    AcsUrl = responseTempData.Model.AcsUrl,
                    MD = responseTempData.Model.TransactionId.ToString(),
                    PaReq = responseTempData.Model.PaReq,
                    TempUrl = context.Request.GetEncodedUrl()
                }).ExecuteAsync(context);
                return;
            }

            if (HttpMethods.IsPost(context.Request.Method))
            {
                if (!context.Request.HasFormContentType)
                {
                    _logger.NotFormContentType();
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                var form = await context.Request.ReadFormAsync();
                if (!(form.TryGetValue(CloudPaymentsDefaults.FormKeyMD, out var md)
                    && form.TryGetValue(CloudPaymentsDefaults.FormKeyPaRes, out var paRes)))
                {
                    _logger.MissingRequiredFormKeys(CloudPaymentsDefaults.FormKeyMD, CloudPaymentsDefaults.FormKeyPaRes);
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                var response = await _client.Secure3D(new Secure3DRequest
                {
                    TransactionId = int.Parse(md.ToString()),
                    PaRes = paRes.ToString()
                });

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
