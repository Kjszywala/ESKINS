using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services;
using ESKINS.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class SellController : Controller
	{
		#region Properties

		IItemLogic itemLogic;
		IItemsServices itemServices;
		IErrorLogsServices errorLogs;
		ICategoriesServices categoriesServices;
		IPhasesServices phasesServices;
		IQualitiesServices qualitiesServices;
		IUsersServices usersServices;
		IItemLocationsServices itemLocationsServices;
		IItemCollectionsServices itemCollectionsServices;
		IExteriorsServices exteriorsServices;
		public static List<ItemsModels>? itemsModels;
		public static List<ItemsModels>? itemsModelsSale = new List<ItemsModels>();

		#endregion

		#region Constructor

		public SellController(
			IItemLogic _itemLogic,
			IErrorLogsServices _errorLogs,
			IItemsServices _itemsServices,
			ICategoriesServices _categoriesServices,
			IPhasesServices _phasesServices,
			IQualitiesServices _qualitiesServices,
			IUsersServices _userServices,
			IItemLocationsServices _itemLocationsServices,
			IItemCollectionsServices _itemCollectionsServices,
			IExteriorsServices _exteriorsServices
		  )
		{
			itemLogic = _itemLogic;
			errorLogs = _errorLogs;
			itemServices = _itemsServices;
			categoriesServices = _categoriesServices;
			phasesServices = _phasesServices;
			qualitiesServices = _qualitiesServices;
			usersServices = _userServices;
			itemLocationsServices = _itemLocationsServices;
			itemCollectionsServices = _itemCollectionsServices;
			exteriorsServices = _exteriorsServices;
		}

		#endregion

		#region Methods

		public IActionResult Index()
		{
			if (!Config.isConfirmed)
			{
				ViewBag.ErrorMessage = "To access this bookmark, you need to log in.";
				return View("/Views/Account/Index.cshtml");
			}
			var model = itemServices.GetAllAsync().Result;
			var userItems = model.Where(i => i.UserId == Config.UserId).ToList();
			itemsModels = userItems;
			return View(userItems);
		}

		public async Task<IActionResult> RemoveFromSale(int itemId)
		{
			try
			{
				var item = await itemServices.GetAsync(itemId);

				item.Category = await categoriesServices.GetAsync(item.CategoryId.Value);
				item.ItemLocation = await itemLocationsServices.GetAsync(item.ItemLocationId.Value);
				item.ItemCollection = await itemCollectionsServices.GetAsync(item.ItemCollectionId.Value);
				item.Phase = await phasesServices.GetAsync(item.PhaseId.Value);
				item.Quality = await qualitiesServices.GetAsync(item.QualityId.Value);
				item.Exterior = await exteriorsServices.GetAsync(item.ExteriorId.Value);
				item.User = await usersServices.GetAsync(item.UserId.Value);
				item.OnSale = false;
				itemsModels = itemLogic.RemoveFromSaleAsync(item).Result.Where(i => i.UserId == Config.UserId).ToList();
				return PartialView("_ItemPartial", itemsModels);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return View("Index");
			}
		}
		public async Task<ActionResult> SearchItemsAsync(string query)
		{
			try
			{
				var list = itemsModels;
				if (string.IsNullOrEmpty(query))
				{
					return PartialView("_ItemPartial", itemsModels);
				}
				list = itemLogic.SearchItems(list, query);
				foreach (var item in list)
				{
					item.User = await usersServices.GetAsync(item.UserId.Value);
				}
				var items = list.Where(i => i.UserId == Config.UserId).ToList();
				return PartialView("_ItemPartial", items);
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
				foreach (var item in list)
				{
					item.User = await usersServices.GetAsync(item.UserId.Value);
				}
				var items = list.Where(i => i.UserId == Config.UserId).ToList();
				return PartialView("_ItemPartial", items);
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
				foreach (var item in list)
				{
					item.User = await usersServices.GetAsync(item.UserId.Value);
				}
				var items = list.Where(i => i.UserId == Config.UserId).ToList();
				return PartialView("_ItemPartial", items);
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
				foreach (var item in list)
				{
					item.User = await usersServices.GetAsync(item.UserId.Value);
				}
				var items = list.Where(i => i.UserId == Config.UserId).ToList();
				return PartialView("_ItemPartial", items);
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
				foreach (var item in list)
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

		public async Task<IActionResult> AddForSaleAsync(int id)
		{
			try
			{
				var item = itemServices.GetAsync(id).Result;
				//sell.CurrentAmount = sell.CurrentAmount - (item.ActualPrice - (item.ActualPrice * Decimal.Parse("0.25")));
				itemsModelsSale.Add(item);
				return ViewComponent("SaleCartComponent", itemsModelsSale);
			}
			catch (Exception ex)
			{
				await errorLogs.Error(ex);
				return Redirect("/Sell");
			}
		}

		#endregion

	}
}
