using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using System.Linq;

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
                var model = itemsServices.GetAllAsync().Result.OrderByDescending(item => item.SerialNumber).First() ?? null;

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
        public List<ItemsModels> GetBestDeals(List<ItemsModels> list)
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
        public List<ItemsModels> GetNewestFirst(List<ItemsModels> list)
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
        public List<ItemsModels> GetOldestFirst(List<ItemsModels> list)
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
        public List<ItemsModels> GetLowestPriceFirst(List<ItemsModels> list)
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
        public List<ItemsModels> GetHighestPriceFirst(List<ItemsModels> list)
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
		public List<ItemsModels> GetBestDiscount(List<ItemsModels> list)
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
		public List<ItemsModels> SearchItems(List<ItemsModels> list, string text)
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
		public List<ItemsModels> FilterCategories(List<ItemsModels> list, string[] selectedCategories)
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

		#endregion
	}
}
