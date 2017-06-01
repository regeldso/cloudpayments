// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using Microsoft.Extensions.Logging;
using System;

namespace CloudPayments.Module
{
    internal static class CloudPaymentsLoggerExtensions
    {
        private static readonly Action<ILogger, Exception> _notFormContentType;
        private static readonly Action<ILogger, string, Exception> _missingRequiredFormKeys;
        private static readonly Action<ILogger, string, Exception> _localRedirectExecuting;
        private static readonly Action<ILogger, Exception> _paymentSuccess;
        private static readonly Action<ILogger, string, Exception> _paymentFailed;
        private static readonly Action<ILogger, string, Exception> _emptyTempData;

        static CloudPaymentsLoggerExtensions()
        {
            _notFormContentType = LoggerMessage.Define(
                LogLevel.Warning,
                1,
                "Content-Type header doesn't belong to form types");

            _missingRequiredFormKeys = LoggerMessage.Define<string>(
                LogLevel.Warning,
                2,
                "Missing required form keys: {Keys}");

            _localRedirectExecuting = LoggerMessage.Define<string>(
                LogLevel.Information,
                3,
                "Executing local redirect to {Url}");

            _paymentSuccess = LoggerMessage.Define(
                LogLevel.Information,
                4,
                "Payment successfuly completed");

            _paymentFailed = LoggerMessage.Define<string>(
                LogLevel.Warning,
                5,
                "Payment failed with message '{Message}'.");

            _emptyTempData = LoggerMessage.Define<string>(
                LogLevel.Warning,
                6,
                "The TempData doest'n contain data for {Key}");
        }

        public static void NotFormContentType(this ILogger logger) =>
            _notFormContentType(logger, null);

        public static void PaymentSuccess(this ILogger logger) =>
            _paymentSuccess(logger, null);

        public static void PaymentFailed(this ILogger logger, string message) =>
            _paymentFailed(logger, message, null);

        public static void MissingRequiredFormKeys(this ILogger logger, params string[] keys) =>
            _missingRequiredFormKeys(logger, string.Join(", ", keys), null);

        public static void EmptyTempData(this ILogger logger, string key) =>
            _emptyTempData(logger, key, null);

        public static void LocalRedirectExecuting(this ILogger logger, string url) =>
            _localRedirectExecuting(logger, url, null);
    }
}
