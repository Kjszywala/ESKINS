using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class ContactComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("ContactComponent");
        }
    }
}
