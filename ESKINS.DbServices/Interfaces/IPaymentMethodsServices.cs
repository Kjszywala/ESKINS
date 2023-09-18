using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Interfaces
{
    /// <summary>
    /// Interface for all PaymentMethods calls to API. 
    /// </summary>
    public interface IPaymentMethodsServices : IBaseServices<PaymentMethods>
    {
        /// <summary>
        /// Gets all active payment methods.
        /// </summary>
        /// <returns></returns>
        Task<List<PaymentMethods>> GetAllActivePaymentMethods();
    }
}
