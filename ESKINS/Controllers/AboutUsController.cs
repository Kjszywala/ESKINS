using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
