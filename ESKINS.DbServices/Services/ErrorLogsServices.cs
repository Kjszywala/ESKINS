using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using log4net;
using System;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Policy;

namespace ESKINS.DbServices.Services
{
    public class ErrorLogsServices :
        IErrorLogsServices
    {
        #region 
        /// <summary>
        /// Http client to send post requests.
        /// </summary>
        public readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Api url.
        /// </summary>
        private const string URL = "https://localhost:7108/swagger/v1/swagger.json";

        /// <summary>
        /// Ilogger to collect errors in database.
        /// </summary>
        public static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion
        #region Constructor

        public ErrorLogsServices()
        {
            _httpClient.BaseAddress = new Uri(URL);
        }

        #endregion
        #region Methods

        /// <summary>
        /// Adds error to database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>True if operation completed, else false</returns>
        public async Task<bool> Error(Exception exception)
        {
            ErrorLogsModels error = new ErrorLogsModels()
            {
                Date = DateTime.Now,
                Message = exception.Message ?? "Cannot get message.",
                Exception = exception.StackTrace ?? "Cannot get stack trace."
            };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/v1.0/ErrorLogs/", error);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Removes errors from database.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <returns>True if operation completed, else false</returns>
        public async Task<bool> RemoveError(int Id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("/api/v1.0/ErrorLogs/" + Id);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Gets all items from database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>List of active payment methods model</returns>
        public async Task<List<ErrorLogsModels>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/v1.0/ErrorLogs/");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadFromJsonAsync<List<ErrorLogsModels>>();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new Exception($"Endpoint: /api/v1.0/ErrorLogs/\nFailed to retrieve data from API. Task<List<T>> GetAllAsync()", ex);
            }
        }
        #endregion
    }
}
