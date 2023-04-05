using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;

namespace ESKINS.DbServices.Services
{
    public class ItemLogsServices :
        BaseServices<ItemLogsModels>,
        IItemLogsServices
    {
        #region Constructor

        public ItemLogsServices() 
            : base("/api/v1.0/ItemLogs/")
        {
        }

        #endregion
    }
}
