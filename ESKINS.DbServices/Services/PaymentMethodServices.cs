using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;
using System.Net.Http.Json;

namespace ESKINS.DbServices.Services
{
    /// <summary>
    /// Class handles all API calls for payment methods.
    /// </summary>
    public class PaymentMethodServices : 
        BaseServices<PaymentMethods>, 
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
        public async Task<List<PaymentMethods>> GetAllActivePaymentMethods()
        {
            try
            {
                var response = await _httpClient.GetAsync(URI + "active?query=active");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<List<PaymentMethods>>();
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
