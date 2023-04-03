﻿using ESKINS.DbServices.Interfaces;
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
        public readonly HttpClient _httpClient;
        /// <summary>
        /// Ilogger for posting any errors to db.
        /// </summary>
        public readonly ILogger<T> _logger;
        /// <summary>
        /// Url to API calls.
        /// </summary>
        public const string URL = "https://localhost:7108/swagger/v1/swagger.json";
        /// <summary>
        /// Uri of the API.
        /// </summary>
        public readonly string URI;

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

        // Inheritdoc
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
                _logger.LogError(ex, "Failed to get data from API in: Task<bool> AddAsync(T Item)");
                return false;
            }
        }

        // Inheritdoc
        public async Task<bool> EditAsync(int Id, T Item)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(URI+Id, Item);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get data from API in: Task<bool> AddAsync(T Item)");
                return false;
            }
        }

        // Inheritdoc
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
                throw new Exception("Failed to retrieve data from API.", ex);
            }
        }

        // Inheritdoc
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
                _logger.LogError(ex, "Failed to get data from API in: Task<bool> AddAsync(T Item)");
                throw new Exception("Failed to retrieve data from API.", ex);
            }
        }

        // Inheritdoc
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
                _logger.LogError(ex, "Failed to get data from API in: Task<bool> AddAsync(T Item)");
                return false;
            }
        }

        #endregion

    }
}
