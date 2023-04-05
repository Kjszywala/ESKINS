using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class ItemsServices :
        BaseServices<ItemsModels>,
        IItemsServices
    {
        #region Constructor

        public ItemsServices(string _URI) : base(_URI)
        {
        }

        #endregion
    }
}
