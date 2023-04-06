using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class PaymentMethodsController : Controller
    {
        #region Variables

        IPaymentMethodsServices paymentMethodsServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public PaymentMethodsController(
            IPaymentMethodsServices _paymentMethodsServices,
            IErrorLogsServices _errorLogsServices)
        {
            paymentMethodsServices = _paymentMethodsServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Controllers

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var model = await paymentMethodsServices.GetAllActivePaymentMethods();
                return View(model);
            }
            catch(Exception e)
            {
                await errorLogsServices.Error(e);
                return View();
            }
        }

        #endregion

    }
}
