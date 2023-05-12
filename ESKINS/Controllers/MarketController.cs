using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class MarketController : Controller
	{
		public IActionResult Index()
		{
			if (!Config.isConfirmed) 
			{
				ViewBag.ErrorMessage = "To access this bookmark, you need to log in.";
				return View("/Views/Account/Index.cshtml");
			}
			return View();
		
		}
	}
}
