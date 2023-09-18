using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class SellersServices :
        BaseServices<Sellers>,
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
