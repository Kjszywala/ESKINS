using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
