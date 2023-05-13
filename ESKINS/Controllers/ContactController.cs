using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class ContactController : Controller
    {
		IItemsServices itemsServices;

		public ContactController(IItemsServices _itemsServices)
		{
			itemsServices = _itemsServices;
		}
		public IActionResult Index()
        {
			return View();
		}
    }
}
