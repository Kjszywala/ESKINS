using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class ItemLocationsServices :
        BaseServices<ItemLoactionsModels>,
        IItemLocationsServices
    {
        #region Constructor

        public ItemLocationsServices() 
            : base("/api/v1.0/ItemLocations/")
        {
        }

        #endregion
    }
}
