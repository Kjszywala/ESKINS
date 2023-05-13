using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class ProfileController : Controller
    {
		IItemsServices itemsServices;

		public ProfileController(IItemsServices _itemsServices)
		{
			itemsServices = _itemsServices;
		}
		public IActionResult Index()
        {
			return View();
		}
    }
}
