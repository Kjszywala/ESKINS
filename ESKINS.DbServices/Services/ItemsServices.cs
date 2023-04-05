using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class ItemsServices :
        BaseServices<ItemsModels>,
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
