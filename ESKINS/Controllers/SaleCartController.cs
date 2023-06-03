using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class SaleCartController : Controller
	{
		#region Variables

		ISaleCartLogic saleCartLogic;
		ISaleCartServices saleCartService;
		IErrorLogsServices errorLogsServices;
		IItemsServices itemsServices;
		public static List<SaleCartModels>? itemsModels;

		#endregion

		#region Constructor

		public SaleCartController(
			ISaleCartLogic _saleCartLogic,
			ISaleCartServices _saleCartService,
			IErrorLogsServices _errorLogsServices,
			IItemsServices _itemsServices)
		{
			saleCartLogic = _saleCartLogic;
			saleCartService = _saleCartService;
			errorLogsServices = _errorLogsServices;
			itemsServices = _itemsServices;
		}

		#endregion

		#region Methods

		public IActionResult UpdateSaleCartComponent()
		{
			// Perform the necessary logic here
			// Retrieve the updated data
			// Return a partial view with the updated content
			var itemsModels = saleCartService.GetAllAsync().Result.Where(item => item.SessionId == Config.SessionId).ToList();
			return ViewComponent("SaleCartComponent", itemsModels);
		}

		public async Task<IActionResult> AddAsync(int id)
		{
			try
			{
				var saleCartItems = saleCartService.GetAllAsync().Result.Where(i => i.SessionId == Config.SessionId).ToList(); 
				foreach(var cartItem in saleCartItems)
				{
					if (id == cartItem.ItemId)
					{
						TempData["Message"] = "Item already in cart";
						return RedirectToAction("Index", "Sell");
					}
				}
				await saleCartLogic.AddToCart(id);
				var item = await itemsServices.GetAsync(id);
				Config.SaleCartOverall += item.ActualPrice - (item.ActualPrice * Decimal.Parse("0.30"));
				return RedirectToAction("Index", "Sell");
			}
			catch (Exception ex)
			{
				await errorLogsServices.Error(ex);
				return View("Error");
			}
		}

        public async Task<IActionResult> RemoveAsync(int id)
        {
            try
            {
                var item = await saleCartService.GetAsync(id);
                await saleCartLogic.RemoveFromCart(id);
                Config.SaleCartOverall -= item.ItemActualPrice - (item.ItemActualPrice * Decimal.Parse("0.30"));
                return RedirectToAction("Index", "Sell");
            }
            catch (Exception ex)
            {
                await errorLogsServices.Error(ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> RemoveAllAsync()
        {
            try
            {
				await saleCartLogic.RemoveAll();
				Config.SaleCartOverall = 0;
                return RedirectToAction("Index", "Sell");
            }
            catch (Exception ex)
            {
                await errorLogsServices.Error(ex);
                return View("Error");
            }
        }

        #endregion
    }
}
