using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class ItemPriceHistoriesServices :
        BaseServices<ItemPriceHistories>,
        IItemPriceHistoriesServices
    {
        #region Constructor

        public ItemPriceHistoriesServices() 
            : base("/api/v1.0/ItemPriceHistories/")
        {
        }

        #endregion
    }
}
