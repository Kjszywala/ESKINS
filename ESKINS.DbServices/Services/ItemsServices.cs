using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class ItemsServices :
        BaseServices<Items>,
        IItemsServices
    {
        #region Constructor

        public ItemsServices() 
            : base("/api/v1.0/Items/")
        {
        }

        #endregion
    }
}
