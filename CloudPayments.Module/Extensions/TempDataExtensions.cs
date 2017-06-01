// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace CloudPayments.Module
{
    public static class TempDataExtensions
    {
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) =>
            tempData[key] = JsonConvert.SerializeObject(value);

        public static T Get<T>(this ITempDataDictionary tempData, string key)
        {
            if (!tempData.TryGetValue(key, out var value))
                return default(T);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value as string);
        }

        internal static void Save<T>(this ITempDataDictionary tempData, string key, T value)
        {
            tempData.Set(key, value);
            tempData.Save();
        }
    }
}
