using ESKINS.BusinessLogic.BusinessLogic;
using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class SellController : Controller
	{
		IItemsServices itemsServices;
		IItemLogic itemLogic;
		IErrorLogsServices errorLogsServices;
		public static List<ItemsModels>? itemsModels;

		public SellController(IItemsServices _itemsServices, IItemLogic _itemLogic, IErrorLogsServices _errorLogsServices)
		{
			itemsServices = _itemsServices;
			itemLogic = _itemLogic;
			errorLogsServices = _errorLogsServices;
		}
		public IActionResult Index()
		{
			//if (!Config.isConfirmed)
			//{
			//	ViewBag.ErrorMessage = "To access this bookmark, you need to log in.";
			//	return View("/Views/Account/Index.cshtml");
			//}
			var model = itemsServices.GetAllAsync().Result;
			var userItems = model.Where(i => i.UserId == Config.UserId).ToList();
			itemsModels = userItems;
			return View(userItems);
		}

		public ActionResult SearchItems(string query)
		{
			try
			{
				var list = itemsModels;
				if (string.IsNullOrEmpty(query))
				{
					return PartialView("_ItemSellPartial", itemsModels);
				}
				list = itemLogic.SearchItems(list, query);
				return PartialView("_ItemSellPartial", list);
			}
			catch (Exception ex)
			{
				errorLogsServices.Error(ex);
				return View("Index");
			}
		}
	}
}
