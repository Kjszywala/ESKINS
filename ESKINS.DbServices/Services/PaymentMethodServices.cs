using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services.Abstract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace DbServices.Services
{
    /// <summary>
    /// Class handles all API calls for payment methods.
    /// </summary>
    public class PaymentMethodServices : AbstractBaseServices<PaymentMethodsModels>, IPaymentMethodsServices
    {
        #region Constructor

        public PaymentMethodServices(ILogger<PaymentMethodsModels> logger)
            : base(logger, "/api/v1.0/PaymentMethods")
        {
        }

        #endregion
    }
}
