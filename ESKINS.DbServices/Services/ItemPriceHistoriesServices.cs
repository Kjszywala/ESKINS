using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESKINS.DbServices.Services
{
    public class ItemPriceHistoriesServices :
        BaseServices<ItemPriceHistoriesModels>,
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
