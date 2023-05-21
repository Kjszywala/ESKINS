using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class PaymentMethodComponent : ViewComponent
    {
        #region Variables

        IPaymentMethodsServices paymentServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public PaymentMethodComponent(IPaymentMethodsServices _paymentServices, IErrorLogsServices _errorLogsServices)
        {
            paymentServices = _paymentServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                return View("PaymentMethodComponent", await paymentServices.GetAllAsync());
            }
            catch (Exception ex)
            {
                await errorLogsServices.Error(ex);
                return View("Error");
            }
        }
        #endregion
    }
}
