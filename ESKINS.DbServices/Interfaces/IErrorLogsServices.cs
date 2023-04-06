using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Interfaces
{
    public interface IErrorLogsServices
    {
        /// <summary>
        /// Adds Exception to database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> Error(Exception exception);

        /// <summary>
        /// Removes Exception method from database.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> RemoveError(int Id);

        /// <summary>
        /// Gets all Exceptions from database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>List of active payment methods model</returns>
        Task<List<ErrorLogsModels>> GetAllAsync();
    }
}
