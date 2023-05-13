using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class PrivacyController : Controller
    {
		IItemsServices itemsServices;

		public PrivacyController(IItemsServices _itemsServices)
		{
			itemsServices = _itemsServices;
		}
		public IActionResult Index()
        {
			return View();
		}
    }
}
