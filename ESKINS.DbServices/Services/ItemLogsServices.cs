using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.DbServices.Services
{
    public class ItemLogsServices :
        BaseServices<ItemLogs>,
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
