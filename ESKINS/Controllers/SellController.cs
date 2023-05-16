using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class SellController : Controller
	{
		IItemLogic itemLogic;
		IItemsServices itemServices;
		IErrorLogsServices errorLogs;
		ICategoriesServices categoriesServices;
		IPhasesServices phasesServices;
		IQualitiesServices qualitiesServices;
		IUsersServices usersServices;
		public static List<ItemsModels>? itemsModels;

		public SellController(
			IItemLogic _itemLogic,
			IErrorLogsServices _errorLogs,
			IItemsServices _itemsServices,
			ICategoriesServices _categoriesServices,
			IPhasesServices _phasesServices,
			IQualitiesServices _qualitiesServices,
			IUsersServices _userServices
		  )
		{
			itemLogic = _itemLogic;
			errorLogs = _errorLogs;
			itemServices = _itemsServices;
			categoriesServices = _categoriesServices;
			phasesServices = _phasesServices;
			qualitiesServices = _qualitiesServices;
			usersServices = _userServices;
		}
		public IActionResult Index()
		{
			//if (!Config.isConfirmed)
			//{
			//	ViewBag.ErrorMessage = "To access this bookmark, you need to log in.";
			//	return View("/Views/Account/Index.cshtml");
			//}
			var model = itemServices.GetAllAsync().Result;
			var userItems = model.Where(i => i.UserId == Config.UserId).ToList();
			itemsModels = userItems;
			return View(userItems);
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
				foreach ( var item in list )
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
	}
}
