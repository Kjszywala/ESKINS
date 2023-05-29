using ESKINS.DbServices.Models;

namespace ESKINS.BusinessLogic.Interfaces
{
    public interface ICartLogic
    {
        /// <summary>
        /// Adding the item to a cart.
        /// </summary>
        /// <param name="ItemId">Id of the item we want to add</param>
        /// <returns>true if succeed else false</returns>
        public Task<bool> AddToCart(int ItemId);

        /// <summary>
        /// Removes item from cart.
        /// </summary>
        /// <param name="ItemId">Item id which we want to remove from cart.</param>
        /// <returns>true if succeed</returns>
        public Task<bool> RemoveFromCart(int ItemId);
    }
}
