using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class ItemLocationsServices :
        BaseServices<ItemLocations>,
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
