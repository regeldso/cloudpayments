using Microsoft.AspNetCore.Mvc;
using CloudPayments.Module;
using CloudPayments.Client;

namespace CloudPayments.Demo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            TempData.Set(CloudPaymentsDefaults.TempDataKey, new Payment
            {
                AccountId = "10",
                Amount = 100,
                Currency = CurrencyCodes.RUB,
                Description = "Pizza",
                Email = "example@mail.ru",
                InvoiceId = "391849"
            });

            return LocalRedirect("/pay");
        }

        [HttpGet]
        public IActionResult PaymentComplete()
        {
            return Json(TempData.Get<PaymentResponse>(CloudPaymentsDefaults.TempDataKey));
        }
    }
}
