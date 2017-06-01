# CloudPayments
Модуль для оплаты через систему CloudPayments.

`PublicId` и `ApiKey` необходимо заполнить данными полученными в личном кабинете. 
```csharp
services.AddCloudPayments(options =>
{
    options.PublicId = "Public Id";
    options.ApiKey = "Api Key";
});
```
