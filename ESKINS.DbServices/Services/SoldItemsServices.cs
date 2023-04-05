using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class SoldItemsServices :
        BaseServices<SoldItemsModels>,
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
