using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using log4net;
using System.Net.Http.Json;
using System.Reflection;

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
        /// Ilogger to collect errors in database.
        /// </summary>
        public static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
                Message = exception.Message,
                Exception = exception.StackTrace ?? "Cannot get stack trace"
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

        #endregion
    }
}
