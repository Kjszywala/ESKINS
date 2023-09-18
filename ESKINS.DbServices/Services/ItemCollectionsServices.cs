using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class ItemCollectionsServices :
        BaseServices<ItemCollections>,
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
