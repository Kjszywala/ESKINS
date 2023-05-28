using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class BaseController : Controller
    {
        #region Variables

        protected IPaymentMethodsServices paymentMethodsServices;
        protected IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public BaseController(
            IPaymentMethodsServices _paymentMethodsServices,
            IErrorLogsServices _errorLogsServices)
        {
            paymentMethodsServices = _paymentMethodsServices;
            errorLogsServices = _errorLogsServices;
        }
        #endregion
    }
}
