using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class ItemLocationsServices :
        BaseServices<ItemLoactionsModels>,
        IItemLocationsServices
    {
        #region Constructor

        public ItemLocationsServices(string _URI) : base(_URI)
        {
        }

        #endregion
    }
}
