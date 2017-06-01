// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System.Threading.Tasks;

namespace CloudPayments.Client
{
    public interface ICloudPaymentsClient
    {
        Task<PaymentResponse> PayCryptogram(PaymentCryptogramRequest request, bool twoStage = false);
        Task<PaymentResponse> Secure3D(Secure3DRequest request);
        Task<PaymentResponse> Test();
    }
}