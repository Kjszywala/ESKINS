using DbServices.Interfaces;
using DbServices.Models;

namespace DbServices.Services
{
    /// <summary>
    /// Class handles all API calls for payment methods.
    /// </summary>
    public class PaymentMethodService : IPaymentMethodsServices
    {
        #region Variables

        private readonly HttpClient _httpClient;
        private readonly ILogger<PaymentMethodService> _logger;
        //private const string URL = "https://localhost:7108/swagger/v1/swagger.json";
        private const string URL = WebApplication.CreateBuilder().Environment.;

        #endregion

        #region Constructor

        public PaymentMethodService(HttpClient client, ILogger<PaymentMethodService> logger)
        {
            _httpClient = client;
            _logger = logger;
            _httpClient.BaseAddress = new Uri(URL);
        }

        #endregion

        #region Methods


        // inheritdoc
        public Task<List<PaymentMethodsModel>> GetActivePaymentMethodsAsync()
        {
            throw new NotImplementedException();
        }

        // inheritdoc
        public async Task<List<PaymentMethodsModel>> GetPaymentMethodsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/PaymentMethods");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<List<PaymentMethodsModel>>();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get data from API in: Task<List<PaymentMethodsModel>> AddPaymentMethodsAsync()");
                return new List<PaymentMethodsModel>();
            }
        }

        // inheritdoc
        public async Task<bool> AddPaymentMethodsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7108/swagger/v1/swagger.json");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<List<PaymentMethodsModel>>();
                return true;
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Failed to get data from API in: Task<List<PaymentMethodsModel>> AddPaymentMethodsAsync()");
                return false;
            }
        }

        // inheritdoc
        public Task<bool> EditPaymentMethodAsync()
        {
            throw new NotImplementedException();
        }

        // inheritdoc
        public Task<bool> RemovePaymentMethodAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
