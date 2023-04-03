using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services.Abstract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Net.Http.Json;
using System.Text;

namespace DbServices.Services
{
    /// <summary>
    /// Class handles all API calls for payment methods.
    /// </summary>
    public class PaymentMethodServices : AbstractBaseServices<PaymentMethodsModels>, IPaymentMethodsServices<PaymentMethodsModels>
    {
        #region Constructor

        public PaymentMethodServices(ILogger<PaymentMethodsModels> logger)
            : base(logger, "/api/v1.0/PaymentMethods/")
        {
        }

        #endregion

        #region Methods

        // Inheritdoc
        public async Task<List<PaymentMethodsModels>> GetAllActivePaymentMethods()
        {
            try
            {
                var response = await _httpClient.GetAsync(URI + "active");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<List<PaymentMethodsModels>>();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get data from API in: Task<bool> AddAsync(T Item)");
                throw new Exception("Failed to retrieve data from API.", ex);
            }
        } 

        #endregion

    }
}
