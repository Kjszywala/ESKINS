using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class ItemCollectionsServices :
        BaseServices<ItemCollectionsModels>,
        IItemCollectionsServices
    {
        #region Constructor
       
        public ItemCollectionsServices() 
            : base("/api/v1.0/ItemCollections/")
        {
        }
        
        #endregion

    }
}
