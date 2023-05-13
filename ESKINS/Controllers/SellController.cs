using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class SellController : Controller
	{
		IItemsServices itemsServices;

		public SellController(IItemsServices _itemsServices)
		{
			itemsServices = _itemsServices;
		}
		public IActionResult Index()
		{
			if (!Config.isConfirmed)
			{
				ViewBag.ErrorMessage = "To access this bookmark, you need to log in.";
				return View("/Views/Account/Index.cshtml");
			}
			var model = itemsServices.GetAllAsync().Result;
			var userItems = model.Where(i => i.UserId == Config.UserId).ToList();
			return View(userItems);
		}
	}
}
