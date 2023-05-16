using ESKINS.DbServices.Models;

namespace ESKINS.BusinessLogic.Interfaces
{
    public interface IItemLogic
    {

        /// <summary>
        /// Get next serial number from db.
        /// </summary>
        /// <returns>next serial number</returns>
        public int GetNextSerialNumber();

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

        /// <summary>
        /// Search for an items with given characters in the item name.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
		public List<ItemsModels> SearchItems(List<ItemsModels> list, string text);

        /// <summary>
        /// Search for categories.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="selectedCategories"></param>
        /// <returns></returns>
        public List<ItemsModels> FilterCategories(List<ItemsModels> list, List<string> selectedCategories);

        /// <summary>
        /// Search for phases.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="selectedCategories"></param>
        /// <returns></returns>
        public List<ItemsModels> FilterPhases(List<ItemsModels> list, List<string> selectedPhases);

        /// <summary>
        /// Search for unique items.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="selectedPhases"></param>
        /// <returns></returns>
        public List<ItemsModels> FilterUnique(List<ItemsModels> list, List<string> selectedUnique);

        /// <summary>
        /// Search location of an item.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<ItemsModels> SearchLocation(List<ItemsModels> list, string text);

        /// <summary>
        /// Search for item collection.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<ItemsModels> SearchCollection(List<ItemsModels> list, string text);
	}
}
