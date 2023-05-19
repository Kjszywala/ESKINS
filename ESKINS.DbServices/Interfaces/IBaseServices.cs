using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Interfaces
{
    public interface IBaseServices<T> 
        where T : class
    {
        /// <summary>
        /// Gets all items from database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>List of active items</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Gets item with the given id from database.
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>Item with given Id</returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Adds item to database.
        /// </summary>
        /// <param name="Item">Model</param>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> AddAsync(T Item);

        /// <summary>
        /// Removes item from database.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> RemoveAsync(int Id);

        /// <summary>
        /// Edits item in database.
        /// </summary>
        /// <param name="Id">Item Id</param>
        /// <param name="Item">Item</param>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> EditAsync(int Id, T Item);
    }
}
