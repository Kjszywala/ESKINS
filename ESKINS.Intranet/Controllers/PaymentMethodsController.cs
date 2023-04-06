using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class PaymentMethodsController : Controller
    {
        #region Variables

        IPaymentMethodsServices paymentMethodsServices;

        #endregion

        #region Constructor

        public PaymentMethodsController(IPaymentMethodsServices _paymentMethodsServices)
        {
            paymentMethodsServices = _paymentMethodsServices;
        }

        #endregion

        #region Controllers

        public async Task<IActionResult> IndexAsync()
        {
            var model = await paymentMethodsServices.GetAllAsync();
            return View(model);
        }

        #endregion

    }
}
