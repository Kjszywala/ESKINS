using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class PaymentMethodsController : Controller
    {
        IPaymentMethodsServices paymentMethodsServices;

        public PaymentMethodsController(IPaymentMethodsServices _paymentMethodsServices)
        {
            paymentMethodsServices = _paymentMethodsServices;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var model = await paymentMethodsServices.GetAllAsync();
            return View(model);
        }
    }
}
