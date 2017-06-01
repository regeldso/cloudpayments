// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CloudPayments.Client.Properties;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace CloudPayments.Client
{
    public class CloudPaymentsClient : ICloudPaymentsClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly JsonSerializerSettings _jsonSettins =
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        private readonly CloudPaymentsClientOptions _options;
        private readonly ILogger _logger;

        public CloudPaymentsClient(
            IOptions<CloudPaymentsClientOptions> options,
            ILoggerFactory loggerFactory)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<CloudPaymentsClient>();
            _options = options.Value;
        }

        public Task<PaymentResponse> Test() => PostAsJsonAsync<PaymentResponse>(CloudPaymentsClientDefaults.TestEndpoint);

        public Task<PaymentResponse> Secure3D(Secure3DRequest request) =>
            PostAsJsonAsync<Secure3DRequest, PaymentResponse>(CloudPaymentsClientDefaults.Secure3DEndpoint, request);

        public Task<PaymentResponse> PayCryptogram(PaymentCryptogramRequest request, bool twoStage = false) =>
            PostAsJsonAsync<PaymentCryptogramRequest, PaymentResponse>(
                twoStage ? CloudPaymentsClientDefaults.PaymentCryptogramTwoStageEndpoint
                : CloudPaymentsClientDefaults.PaymentCryptogramSingleStageEndpoint, request);

        private async Task<TResult> PostAsJsonAsync<T, TResult>(string url, T content) where T : class where TResult : class
        {
            try
            {
                return await PostAsJsonAsync<TResult>(url, JsonConvert.SerializeObject(content));
            }
            catch (JsonException ex)
            {
                _logger.ObjectSerializationException(ex);
                throw;
            }
        }

        private async Task<TResult> PostAsJsonAsync<TResult>(string url, string content = null) where TResult : class
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(Resources.UrlCannotBeNullOrWhiteSpace, nameof(url));

            using (HttpRequestMessage requestMessage =
                new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(content ?? string.Empty, Encoding.UTF8, "application/json")
                })
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_options.PublicId}:{_options.ApiKey}")));

                try
                {
                    using (HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false))
                    {
                        var jsonString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                        try
                        {
                            return JsonConvert.DeserializeObject<TResult>(jsonString, _jsonSettins);
                        }
                        catch (JsonException ex)
                        {
                            _logger.ContentDeserializationException(ex);
                            throw;
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.HttpRequestException(ex);
                    throw;
                }
            }
        }
    }
}
