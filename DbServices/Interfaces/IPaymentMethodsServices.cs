using DbServices.Models;

namespace DbServices.Interfaces
{
    /// <summary>
    /// Interface for all PaymentMethods calls to API. 
    /// </summary>
    public interface IPaymentMethodsServices
    {
        /// <summary>
        /// Gets all payment methods available in database.
        /// </summary>
        /// <returns>List of payment methods model</returns>
        Task<List<PaymentMethodsModel>> GetPaymentMethodsAsync();

        /// <summary>
        /// Gets all active payment methods from database.
        /// </summary>
        /// <returns>List of active payment methods model</returns>
        Task<List<PaymentMethodsModel>> GetActivePaymentMethodsAsync();

        /// <summary>
        /// Adds payment method to database.
        /// </summary>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> AddPaymentMethodsAsync(PaymentMethodsModel paymentMethod);

        /// <summary>
        /// Removes payment method to database.
        /// </summary>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> RemovePaymentMethodAsync();

        /// <summary>
        /// Edits payment method in database.
        /// </summary>
        /// <returns>True if operation completed, else false</returns>
        Task<bool> EditPaymentMethodAsync();
    }
}
