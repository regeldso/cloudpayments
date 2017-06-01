// Copyright (c) 2017 Samburov Konstantin <samburovkv@yandex.ru>.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
namespace CloudPayments.Module.RazorViews
{
    internal class Secure3dFormModel
    {
        public string PaReq { get; set; }

        public string MD { get; set; }

        public string AcsUrl { get; set; }

        public string TempUrl { get; set; }
    }
}
