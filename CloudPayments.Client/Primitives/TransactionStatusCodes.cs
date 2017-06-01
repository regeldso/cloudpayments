// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
namespace CloudPayments.Client
{
    public enum TransactionStatusCodes
    {
        None,
        AwaitingAuthentication,
        Authorized,
        Completed,
        Cancelled,
        Declined
    }
}
