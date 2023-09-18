using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class CustomersServices :
        BaseServices<Customers>,
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
