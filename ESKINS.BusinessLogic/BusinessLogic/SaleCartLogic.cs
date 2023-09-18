using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;

namespace ESKINS.BusinessLogic.BusinessLogic
{
	public class SaleCartLogic : ISaleCartLogic
	{
		#region Properties

		ISaleCartServices saleCartServices;
		IItemsServices itemsServices;

		#endregion

		#region Constructor

		public SaleCartLogic(
			ISaleCartServices _saleCartServices,
			IItemsServices _itemServices)
		{
			saleCartServices = _saleCartServices;
			itemsServices = _itemServices;
		}

		#endregion

		#region Methods

		/// <Inheritdoc />
		public async Task<bool> AddToCart(int ItemId)
		{
			try
			{
				var item = await itemsServices.GetAsync(ItemId);
				SaleCart cartModels = new SaleCart()
				{
					ItemId = ItemId,
					SessionId = BussinesLogicConfig.SessionId,
					ItemActualPrice = item.ActualPrice,
					ItemDiscount = item.Discount,
					ItemImage = item.ItemImage,
					ItemName = item.ProductName
					
				};
				await saleCartServices.AddAsync(cartModels);

				return true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <Inheritdoc />
		public async Task<int> GetQuantity()
		{
			try
			{
				var list = saleCartServices.GetAllAsync().Result;
				return list.Where(item => item.SessionId == BussinesLogicConfig.SessionId).ToList().Count;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <Inheritdoc />
		public async Task<bool> RemoveAll()
		{
			try
			{
				var list = saleCartServices.GetAllAsync().Result.Where(item => item.SessionId == BussinesLogicConfig.SessionId).ToList();
				foreach (var item in list)
				{
					await saleCartServices.RemoveAsync(item.Id);
				}
				return true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <Inheritdoc />
		public async Task<bool> RemoveFromCart(int ItemId)
		{
			try
			{
				await saleCartServices.RemoveAsync(ItemId);
				return true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		#endregion
	}
}
