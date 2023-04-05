using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace DbServices.Services
{
    /// <summary>
    /// Class handles all API calls for payment methods.
    /// </summary>
    public class PaymentMethodServices : 
        BaseServices<PaymentMethodsModels>, 
        IPaymentMethodsServices
    {
        private IBaseServices<PaymentMethodsModels> baseServices;
        #region Constructor

        public PaymentMethodServices(IBaseServices<PaymentMethodsModels> _baseServices)
            : base("/api/v1.0/PaymentMethods/")
        {
            baseServices = _baseServices;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<List<PaymentMethodsModels>> GetAllActivePaymentMethods()
        {
            try
            {
                var response = await _httpClient.GetAsync(URI + "active?query=active");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<List<PaymentMethodsModels>>();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new Exception("Failed to retrieve data from API.", ex);
            }
        } 

        #endregion

    }
}
