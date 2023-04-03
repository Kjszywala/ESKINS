using ESKINS.DbServices.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace ESKINS.DbServices.Services.Abstract
{
    /// <summary>
    /// Class handle primary methods CRUD.
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    public class AbstractBaseServices<T> 
        : IBaseServices<T> where T : class
    {
        #region Variables

        /// <summary>
        /// Http client to send post requests.
        /// </summary>
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Ilogger for posting any errors to db.
        /// </summary>
        private readonly ILogger<T> _logger;
        /// <summary>
        /// Url to API calls.
        /// </summary>
        private const string URL = "https://localhost:7108/swagger/v1/swagger.json";
        /// <summary>
        /// Uri of the API.
        /// </summary>
        private readonly string URI;

        #endregion

        #region Constructor

        public AbstractBaseServices(ILogger<T> logger, string _URI)
        {
            _httpClient = new HttpClient();
            _logger = logger;
            _httpClient.BaseAddress = new Uri(URL);
            URI = _URI;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method adds the item to database.
        /// </summary>
        /// <param name="Item">Item</param>
        /// <returns>True if success</returns>
        public async Task<bool> AddAsync(T Item)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
                           URI, Item);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get data from API in: Task<bool> AddAsync(T Item)");
                return false;
            }
        }

        /// <summary>
        /// Method edits the item with given Id in db.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <returns></returns>
        public Task<bool> EditAsync(int Id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all items from database.
        /// </summary>
        /// <returns>List of all Items</returns>
        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(URI);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<List<T>>();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get data from API in: Task<List<T>> GetAllAsync()");
                return new List<T>();
            }
        }

        /// <summary>
        /// Method gets the item with given Id from db.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <returns></returns>
        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method removes the item with given id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> RemoveAsync(int Id)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
