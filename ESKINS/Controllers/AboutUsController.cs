using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class AboutUsController : Controller
    {
		IItemsServices itemsServices;

		public AboutUsController(IItemsServices _itemsServices)
		{
			itemsServices = _itemsServices;
		}
		public IActionResult Index()
        {
			return View();
		}
    }
}
