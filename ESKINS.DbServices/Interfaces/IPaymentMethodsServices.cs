using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Interfaces
{
    /// <summary>
    /// Interface for all PaymentMethods calls to API. 
    /// </summary>
    public interface IPaymentMethodsServices
    {
        /// <summary>
        /// Gets all active payment methods.
        /// </summary>
        /// <returns></returns>
        Task<List<PaymentMethodsModels>> GetAllActivePaymentMethods();
    }
}
