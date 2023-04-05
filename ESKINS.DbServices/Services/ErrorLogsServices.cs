﻿using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using log4net;
using System.Net.Http.Json;
using System.Reflection;

namespace ESKINS.DbServices.Services
{
    internal class ErrorLogsServices :
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
        public async Task<bool> AddAsync(ErrorLogsModels Item)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/v1.0/Customers/", Item);
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
        public async Task<bool> RemoveAsync(int Id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("/api/v1.0/Customers/" + Id);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
    }
}
