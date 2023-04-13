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
                var model = itemsServices.GetAllAsync().Result.OrderByDescending(item => item.SerialNumber).First();

                if (model.SerialNumber == "0" || model.SerialNumber == null)
                {
                    return 1;
                }

                return Int32.Parse(model.SerialNumber) + 1;
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
