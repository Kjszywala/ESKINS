using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESKINS.Controllers
{
	public class MarketController : Controller
	{
        #region Variables

        IItemLogic itemLogic;
        IItemsServices itemServices;
        IErrorLogsServices errorLogs;
        ICategoriesServices categoriesServices;
        IItemLocationsServices itemLocationsServices;
        IUsersServices usersServices;
        IItemLogsServices itemLogsServices;
        IItemCollectionsServices itemCollectionsServices;
        IPhasesServices phasesServices;
        IQualitiesServices qualitiesServices;
        IExteriorsServices exteriorsServices;
        public static List<ItemsModels> itemsModels;

        #endregion

        #region MyRegion

        public MarketController(
            IItemLogic _itemLogic,
            IErrorLogsServices _errorLogs,
            IItemsServices _itemsServices,
            ICategoriesServices _categoriesServices,
            IItemLocationsServices _itemLocationsServices,
            IUsersServices _usersServices,
            IItemLogsServices _itemLogsServices,
            IItemCollectionsServices _itemCollectionsServices,
            IPhasesServices _phasesServices,
            IQualitiesServices _qualitiesServices,
            IExteriorsServices _exteriorsServices
            )
        {
            itemLogic = _itemLogic;
            errorLogs = _errorLogs;
            itemServices = _itemsServices;
            categoriesServices = _categoriesServices;
            itemLocationsServices = _itemLocationsServices;
            usersServices = _usersServices;
            itemLogsServices = _itemLogsServices;
            itemCollectionsServices = _itemCollectionsServices;
            phasesServices = _phasesServices;
            qualitiesServices = _qualitiesServices;
            exteriorsServices = _exteriorsServices;
		}

        #endregion

        #region Methods
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
				//if (!Config.isConfirmed)
				//{
				//	ViewBag.ErrorMessage = "To access this bookmark, you need to log in.";
				//	return View("/Views/Account/Index.cshtml");
				//}
				itemsModels = itemServices.GetAllAsync().Result.Where(item => item.OnSale == true).ToList();
				foreach ( var item in itemsModels )
				{
					item.ActualPrice = item.ActualPrice - (item.ActualPrice * item.Discount);
				}
                return View(itemsModels);
            }
            catch (Exception ex)
            {
                await errorLogs.Error(ex);
                return View("Index");
            }

        }

		public async Task<IActionResult> BestDeals()
		{
			try
			{
				itemsModels = itemLogic.GetBestDeals(itemsModels);
				return PartialView("_ItemPartial", itemsModels);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}

		}

		public async Task<IActionResult> Newest()
		{
			try
			{
				itemsModels = itemLogic.GetNewestFirst(itemsModels);
				return PartialView("_ItemPartial", itemsModels);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}

		}

		public async Task<IActionResult> Oldest()
		{
			try
			{
				itemsModels = itemLogic.GetOldestFirst(itemsModels);
				return PartialView("_ItemPartial", itemsModels);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}

		}

		public async Task<IActionResult> LowestPrice()
		{
			try
			{
				itemsModels = itemLogic.GetLowestPriceFirst(itemsModels);
				return PartialView("_ItemPartial", itemsModels);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}

		}

		public async Task<IActionResult> HighestPrice()
		{
			try
			{
				itemsModels = itemLogic.GetHighestPriceFirst(itemsModels);
				return PartialView("_ItemPartial", itemsModels);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}

		}

		[HttpPost]
		public async Task<IActionResult> DetailsAsync(ItemsModels item)
		{
			try
			{
				if (!Config.isConfirmed)
				{
					ViewBag.ErrorMessage = "To access this bookmark, you need to log in.";
					return View("/Views/Account/Index.cshtml");
				}
				var model = itemServices.GetAsync(item.Id).Result;

				model.Category = await categoriesServices.GetAsync(model.CategoryId.Value);
				model.ItemLocation = await itemLocationsServices.GetAsync(model.ItemLocationId.Value);
				model.ItemCollection = await itemCollectionsServices.GetAsync(model.ItemCollectionId.Value);
				model.Phase = await phasesServices.GetAsync(model.PhaseId.Value);
				model.Quality = await qualitiesServices.GetAsync(model.QualityId.Value);
				model.Exterior = await exteriorsServices.GetAsync(model.ExteriorId.Value);
				model.User = await usersServices.GetAsync(model.UserId.Value);
				return View(model);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}
		}

		public ActionResult SearchItems(string query)
		{
			try
			{
				var list = itemsModels;
				if (string.IsNullOrEmpty(query))
				{
					return PartialView("_ItemPartial", itemsModels);
				}
				list = itemLogic.SearchItems(list, query);
				return PartialView("_ItemPartial", list);
			}
			catch (Exception ex)
			{
				errorLogs.Error(ex);
				return View("Index");
			}
		}

		[System.Web.Http.HttpPost]
		public async Task<ActionResult> SearchCategoriesAsync(List<string> categories)
		{
			try
			{
				var list = itemsModels;
				foreach (var item in list)
				{
					item.Category = await categoriesServices.GetAsync(item.CategoryId.Value);
				}
				if (categories.Count == 0)
				{
					return PartialView("_ItemPartial", itemsModels);
				}
				list = itemLogic.FilterCategories(list, categories);
				return PartialView("_ItemPartial", list);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}
		}

		[System.Web.Http.HttpPost]
		public async Task<ActionResult> SearchPhasesAsync(List<string> categories)
		{
			try
			{
				var list = itemsModels;
				foreach (var item in list)
				{
					item.Phase = await phasesServices.GetAsync(item.PhaseId.Value);
				}
				if (categories.Count == 0)
				{
					return PartialView("_ItemPartial", itemsModels);
				}
				list = itemLogic.FilterPhases(list, categories);
				return PartialView("_ItemPartial", list);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}
		}

		[System.Web.Http.HttpPost]
		public async Task<ActionResult> SearchUniqueAsync(List<string> categories)
		{
			try
			{
				var list = itemsModels;
				foreach (var item in list)
				{
					item.Quality = await qualitiesServices.GetAsync(item.QualityId.Value);
				}
				if (categories.Count == 0)
				{
					return PartialView("_ItemPartial", itemsModels);
				}
				list = itemLogic.FilterUnique(list, categories);
				return PartialView("_ItemPartial", list);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}
		}

		public async Task<ActionResult> SearchLocationAsync(string checkedLocation)
		{
			try
			{
				var list = itemsModels;
				if (string.IsNullOrEmpty(checkedLocation))
				{
					return PartialView("_ItemPartial", itemsModels);
				}
				foreach(var item in list)
				{
					item.ItemLocation = await itemLocationsServices.GetAsync(item.ItemLocationId.Value);
				}
				list = itemLogic.SearchLocation(list, checkedLocation);
				return PartialView("_ItemPartial", list);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}
		}

		public async Task<ActionResult> SearchCollectionAsync(string checkedCollection)
		{
			try
			{
				var list = itemsModels;
				if (string.IsNullOrEmpty(checkedCollection) || checkedCollection.Contains("showAll"))
				{
					return PartialView("_ItemPartial", itemsModels);
				}
				foreach (var item in list)
				{
					item.ItemCollection = await itemCollectionsServices.GetAsync(item.ItemCollectionId.Value);
				}
				list = itemLogic.SearchCollection(list, checkedCollection);
				return PartialView("_ItemPartial", list);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}
		}
		#endregion
	}
}
