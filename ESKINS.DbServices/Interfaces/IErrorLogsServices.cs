using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Interfaces
{
    internal interface IErrorLogsServices
    {
        /// <summary>
        /// Adds Exception to database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> AddAsync(Exception exception);

        /// <summary>
        /// Removes Exception method from database.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> RemoveAsync(int Id);
    }
}
