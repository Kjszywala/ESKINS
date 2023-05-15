using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ESKINS.Controllers
{
    public class HomeController : Controller
    {
        IItemsServices itemsServices;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IItemsServices _itemsServices)
        {
            itemsServices = _itemsServices;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = itemsServices.GetAllAsync().Result;
			foreach (var item in model)
			{
				item.ActualPrice = item.ActualPrice - (item.ActualPrice * item.Discount);
			}
			return View(model);
        }
		public IActionResult IndexMain()
		{
			return View();
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}