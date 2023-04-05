using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class SellersServices :
        BaseServices<SellersModels>,
        ISellersServices
    {
        #region Constructor

        public SellersServices() 
            : base("/api/v1.0/Sellers/")
        {
        }

        #endregion
    }
}
