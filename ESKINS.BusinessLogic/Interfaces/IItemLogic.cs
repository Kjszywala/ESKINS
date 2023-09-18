using ESKINS.DbServices.Models.CMS;

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
		public List<Items> GetBestDeals(List<Items> list);

        /// <summary>
        /// Get item sorted by newest creted date.
        /// </summary>
        /// <returns></returns>
        public List<Items> GetNewestFirst(List<Items> list);

        /// <summary>
        /// Get items from oldest created.
        /// </summary>
        /// <returns></returns>
        public List<Items> GetOldestFirst(List<Items> list);

        /// <summary>
        /// Get items in order lowest price first.
        /// </summary>
        /// <returns></returns>
        public List<Items> GetLowestPriceFirst(List<Items> list);

        /// <summary>
        /// Get items in order highest price first.
        /// </summary>
        /// <returns></returns>
        public List<Items> GetHighestPriceFirst(List<Items> list);

        /// <summary>
        /// Get items with highest discount first.
        /// </summary>
        /// <returns></returns>
		public List<Items> GetBestDiscount(List<Items> list);

        /// <summary>
        /// Search for an items with given characters in the item name.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
		public List<Items> SearchItems(List<Items> list, string text);

        /// <summary>
        /// Search for categories.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="selectedCategories"></param>
        /// <returns></returns>
        public List<Items> FilterCategories(List<Items> list, List<string> selectedCategories);

        /// <summary>
        /// Search for phases.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="selectedCategories"></param>
        /// <returns></returns>
        public List<Items> FilterPhases(List<Items> list, List<string> selectedPhases);

        /// <summary>
        /// Search for unique items.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="selectedPhases"></param>
        /// <returns></returns>
        public List<Items> FilterUnique(List<Items> list, List<string> selectedUnique);

        /// <summary>
        /// Search location of an item.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<Items> SearchLocation(List<Items> list, string text);

        /// <summary>
        /// Search for item collection.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<Items> SearchCollection(List<Items> list, string text);

        /// <summary>
        /// Remove item from sale.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task<List<Items>> RemoveFromSaleAsync(Items item);
	}
}
