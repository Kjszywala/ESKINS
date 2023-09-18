using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class SoldItemsServices :
        BaseServices<SoldItems>,
        ISolditemsServices
    {
        #region Constructor

        public SoldItemsServices() 
            : base("/api/v1.0/SoldItems/")
        {
        }

        #endregion
    }
}
