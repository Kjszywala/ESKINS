using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESKINS.DbServices.Services
{
    public class ItemCollectionsServices :
        BaseServices<ItemCollectionsModels>,
        IItemColectionsServices
    {
        #region Constructor
       
        public ItemCollectionsServices() 
            : base("/api/v1.0/ItemCollections/")
        {
        }
        
        #endregion

    }
}
