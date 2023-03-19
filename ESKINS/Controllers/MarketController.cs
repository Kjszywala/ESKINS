using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class MarketController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
