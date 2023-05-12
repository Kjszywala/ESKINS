using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class SellController : Controller
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
