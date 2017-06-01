// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using Microsoft.Extensions.Logging;
using System;

namespace CloudPayments.Client
{
    internal static class CloudPaimentsClientLoggerExtensions
    {
        private static readonly Action<ILogger, Exception> _httpRequestException;
        private static readonly Action<ILogger, Exception> _contentDeserializeException;
        private static readonly Action<ILogger, Exception> _objectSerializeException;

        static CloudPaimentsClientLoggerExtensions()
        {
            _httpRequestException = LoggerMessage.Define(
                LogLevel.Error,
                1,
                "An exception was thrown while sending request.");

            _contentDeserializeException = LoggerMessage.Define(
                LogLevel.Error,
                2,
                "An exception was thrown while deserializing the HTTP content.");

            _objectSerializeException = LoggerMessage.Define(
                LogLevel.Error,
                3,
                "An exception was thrown while serializing object.");
        }

        public static void HttpRequestException(this ILogger logger, Exception exception) =>
            _httpRequestException(logger, exception);

        public static void ContentDeserializationException(this ILogger logger, Exception exception) =>
            _contentDeserializeException(logger, exception);

        public static void ObjectSerializationException(this ILogger logger, Exception exception) =>
            _objectSerializeException(logger, exception);
    }
}
