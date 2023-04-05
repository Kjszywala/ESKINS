﻿using ESKINS.DbServices.Interfaces;
using log4net;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Reflection;

namespace ESKINS.DbServices.Services
{
    /// <summary>
    /// Class handle primary methods CRUD.
    /// </summary>
    /// <typeparam name="T">Model</typeparam>
    public class BaseServices<T>  where T : class
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
        /// <summary>
        /// Ilogger to collect errors in database.
        /// </summary>
        public static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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

        /// <inheritdoc />
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
                _logger.Error(ex);
                return false;
            }
        }

        /// <inheritdoc />
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
                _logger.Error(ex);
                return false;
            }
        }

        /// <inheritdoc />
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
                _logger.Error(ex);
                throw new Exception("Failed to retrieve data from API.", ex);
            }
        }

        /// <inheritdoc />
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
                _logger.Error(ex);
                throw new Exception("Failed to retrieve data from API.", ex);
            }
        }

        /// <inheritdoc />
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
                _logger.Error(ex);
                return false;
            }
        }

        #endregion

    }
}
