using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
