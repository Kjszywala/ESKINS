using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;

namespace ESKINS.BusinessLogic.BusinessLogic
{
    public class ItemLogic : IItemLogic
    {
        #region Variables

        IItemsServices itemsServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public ItemLogic(
            IItemsServices _itemsServices, 
            IErrorLogsServices _errorLogsServices
            )
        {
            itemsServices = _itemsServices;
            errorLogsServices = _errorLogsServices;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Get next serial number from db.
        /// </summary>
        /// <returns>next serial number</returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GetNextSerialNumber()
        {
            try
            {
                var list = itemsServices.GetAllAsync().Result;
                if (list.Count == 0)
                {
                    return 1;
                }

                return list.Count + 1;
            }
            catch (Exception e)
            {
                errorLogsServices.Error(e);
                return 0;
            }
        }

        #endregion
    }
}
