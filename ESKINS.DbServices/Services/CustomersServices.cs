using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class CustomersServices :
        BaseServices<CustomersModels>,
        ICustomersServices
    {
        #region Constructor

        public CustomersServices() 
            : base("/api/v1.0/Customers/")
        {
        }

        #endregion
    }
}
