using ESKINS.DbServices.Interfaces;
using log4net;
using System.Net.Http.Json;
using System.Reflection;

namespace ESKINS.DbServices.Services
{
    /// <summary>
    /// Class handle primary methods CRUD.
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    public class BaseServices<T> where T : class
    {
        #region Variables

        /// <summary>
        /// Http client to send post requests.
        /// </summary>
        public readonly HttpClient _httpClient;

        /// <summary>
        /// Url to API calls.
        /// </summary>
        public const string URL = "https://localhost:7108/swagger/v1/swagger.json";

        /// <summary>
        /// Uri of the API endpoint.
        /// </summary>
        public readonly string URI;

        #endregion

        #region Constructor

        public BaseServices(string _URI)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(URL);
            URI = _URI;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds payment method to database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>True if operation completed, else false</returns>
        public async Task<bool> AddAsync(T Item)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(URI, Item);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Task<bool> AddAsync(T Item)", ex);
            }
        }

        /// <summary>
        /// Edits payment method in database.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <param name="Item">Item</param>
        /// <returns>True if operation completed, else false</returns>
        public async Task<bool> EditAsync(int Id, T Item)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(URI + Id, Item);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Task<bool> EditAsync(int Id, T Item)", ex);
            }
        }

        /// <summary>
        /// Gets all payment methods from database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>List of active payment methods model</returns>
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
                throw new Exception("Task<List<T>> GetAllAsync()", ex);
            }
        }

        /// <summary>
        /// Gets item with the given id from database.
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>Item with given Id</returns>
        public async Task<T> GetAsync(int Id)
        {
            try
            {
                var response = await _httpClient.GetAsync(URI + Id);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<T>();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("Task<T> GetAsync(int Id)", ex);
            }
        }

        /// <summary>
        /// Removes payment method from database.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <returns>True if operation completed, else false</returns>
        public async Task<bool> RemoveAsync(int Id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(URI + Id);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Task<bool> RemoveAsync(int Id)", ex);
            }
        }

        #endregion

    }
}
