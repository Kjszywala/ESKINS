using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Interfaces
{
    /// <summary>
    /// Interface for all PaymentMethods calls to API. 
    /// </summary>
    public interface IPaymentMethodsServices<T> 
        where T : class
    {
        /// <summary>
        /// Gets all active payment methods.
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllActivePaymentMethods();
    }
}
