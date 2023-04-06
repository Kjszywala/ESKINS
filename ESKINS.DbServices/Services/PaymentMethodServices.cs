using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services;
using System.Net.Http.Json;

namespace ESKINS.DbServices.Services
{
    /// <summary>
    /// Class handles all API calls for payment methods.
    /// </summary>
    public class PaymentMethodServices : 
        BaseServices<PaymentMethodsModels>, 
        IPaymentMethodsServices
    {
        #region Constructor

        public PaymentMethodServices()
            : base("/api/v1.0/PaymentMethods/")
        {
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
                throw new Exception($"Endpoint: {URI}\nFailed to retrieve data from API. Task<List<PaymentMethodsModels>> GetAllActivePaymentMethods()", ex);
            }
        } 

        #endregion

    }
}
