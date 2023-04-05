using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Interfaces
{
    internal interface IErrorLogsServices
    {
        /// <summary>
        /// Adds payment method to database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> AddAsync(ErrorLogsModels Item);

        /// <summary>
        /// Removes payment method to database.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> RemoveAsync(int Id);
    }
}
