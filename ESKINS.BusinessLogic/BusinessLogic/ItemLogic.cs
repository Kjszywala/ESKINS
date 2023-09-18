using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.BusinessLogic.BusinessLogic
{
    public class ItemLogic : IItemLogic
    {
		#region Variables

		IItemsServices itemsServices;

		#endregion

		#region Constructor

		public ItemLogic(IItemsServices _itemsServices)
		{
			itemsServices = _itemsServices;
		}

		#endregion

		#region Methods

		/// <Inheritdoc />
		public int GetNextSerialNumber()
        {
            try
            {
                var model = itemsServices.GetAllAsync().Result.OrderByDescending(item => item.Id).First() ?? null;

                if (model == null || model.SerialNumber == "0" || model.SerialNumber == null)
                {
                    return 1;
                }

                return Int32.Parse(model.SerialNumber) + 1;
            }
            catch(InvalidOperationException ex)
            {
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <Inheritdoc />
        public List<Items> GetBestDeals(List<Items> list)
        {
            try
            {
                var model = list.OrderByDescending(item => item.Discount).ToList();

                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <Inheritdoc />
        public List<Items> GetNewestFirst(List<Items> list)
        {
            try
            {
                var model = list.OrderByDescending(item => item.ModificationDate).ToList();

                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <Inheritdoc />
        public List<Items> GetOldestFirst(List<Items> list)
        {
            try
            {
                var model = list.OrderBy(item => item.ModificationDate).ToList();

                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <Inheritdoc />
        public List<Items> GetLowestPriceFirst(List<Items> list)
        {
            try
            {
                var model = list.OrderBy(item => item.ActualPrice).ToList();

                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <Inheritdoc />
        public List<Items> GetHighestPriceFirst(List<Items> list)
        {
            try
            {
                var model = list.OrderByDescending(item => item.ActualPrice).ToList();

                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <Inheritdoc />
		public List<Items> GetBestDiscount(List<Items> list)
		{
			try
			{
				var model = list.OrderByDescending(item => item.Discount).ToList();

				return model;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <Inheritdoc />
		public List<Items> SearchItems(List<Items> list, string text)
		{
			try
			{
				//var model = list.Where(i => i.ProductName.Contains(text)).ToList();
				var model = list.Where(i => i.ProductName.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
				return model;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <Inheritdoc />
		public List<Items> SearchLocation(List<Items> list, string text)
		{
			try
			{
				//var model = list.Where(i => i.ProductName.Contains(text)).ToList();
				var model = list.Where(i => i.ItemLocation.ItemLocation == text).ToList();
				return model;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		} 
        
        /// <Inheritdoc />
		public List<Items> SearchCollection(List<Items> list, string text)
		{
			try
			{
				//var model = list.Where(i => i.ProductName.Contains(text)).ToList();
				var model = list.Where(i => i.ItemCollection.ItemCollection == text).ToList();
				return model;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <Inheritdoc />
		public List<Items> FilterCategories(List<Items> list, List<string> selectedCategories)
		{
			try
			{
				// Filter by selected categories
				if (selectedCategories != null && selectedCategories.Any())
				{
					return list.Where(i => selectedCategories.Contains(i.Category.CategoryDescription)).ToList();
				}

				return list;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <Inheritdoc />
		public List<Items> FilterPhases(List<Items> list, List<string> selectedPhases)
		{
			try
			{
				// Filter by selected categories
				if (selectedPhases != null && selectedPhases.Any())
				{
					return list.Where(i => selectedPhases.Contains(i.Phase.Phase)).ToList();
				}

				return list;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

        /// <Inheritdoc />
		public List<Items> FilterUnique(List<Items> list, List<string> selectedUnique)
        {
            try
            {
                // Filter by selected categories
                if (selectedUnique != null && selectedUnique.Any())
                {
                    return list.Where(i => selectedUnique.Contains(i.Quality.Quality)).ToList();
                }

                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <Inheritdoc />
		public async Task<List<Items>> RemoveFromSaleAsync(Items item)
		{
            List<Items> list = new List<Items>();

			var result = itemsServices.EditAsync(item.Id, item).Result;
            if (result)
            {
                list = await itemsServices.GetAllAsync();
            }
            return list;
		}

		#endregion
	}
}
