using ESKINS.DbServices.Models;

namespace ESKINS.BusinessLogic.Interfaces
{
    public interface IItemLogic
    {

        /// <summary>
        /// Get next serial number from db.
        /// </summary>
        /// <returns>next serial number</returns>
        int GetNextSerialNumber();

		/// <summary>
		/// Get items with best discount sorted by descending.
		/// </summary>
		/// <returns></returns>
		public List<ItemsModels> GetBestDeals(List<ItemsModels> list);

        /// <summary>
        /// Get item sorted by newest creted date.
        /// </summary>
        /// <returns></returns>
        public List<ItemsModels> GetNewestFirst(List<ItemsModels> list);

        /// <summary>
        /// Get items from oldest created.
        /// </summary>
        /// <returns></returns>
        public List<ItemsModels> GetOldestFirst(List<ItemsModels> list);

        /// <summary>
        /// Get items in order lowest price first.
        /// </summary>
        /// <returns></returns>
        public List<ItemsModels> GetLowestPriceFirst(List<ItemsModels> list);

        /// <summary>
        /// Get items in order highest price first.
        /// </summary>
        /// <returns></returns>
        public List<ItemsModels> GetHighestPriceFirst(List<ItemsModels> list);

        /// <summary>
        /// Get items with highest discount first.
        /// </summary>
        /// <returns></returns>
		public List<ItemsModels> GetBestDiscount(List<ItemsModels> list);


	}
}
