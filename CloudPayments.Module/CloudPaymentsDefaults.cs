// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
namespace CloudPayments.Module
{
    public static class CloudPaymentsDefaults
    {
        internal static readonly string PayPath = "/pay";
        internal static readonly string Secure3dPath = "/3dsecure";

        internal static readonly string FormKeyName = "name";
        internal static readonly string FormKeyCryptogram = "cryptogram";
        internal static readonly string FormKeyMD = "MD";
        internal static readonly string FormKeyPaRes = "PaRes";

        public static readonly string TempDataKey = "__CloudPayments.Module.CloudPaymentsMiddleware.TempDataKey";
    }
}
