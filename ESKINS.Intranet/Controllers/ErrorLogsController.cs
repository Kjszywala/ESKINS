using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class ErrorLogsController : Controller
    {
        #region Variables

        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public ErrorLogsController(
            IErrorLogsServices _errorLogsServices)
        {
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Controllers

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var model = await errorLogsServices.GetAllAsync();
                return View(model);
            }
            catch (InvalidOperationException e)
            {
                await errorLogsServices.Error(e);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var model = await errorLogsServices.RemoveError(id);
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException e)
            {
                await errorLogsServices.Error(e);
                return View();
            }
        }
        #endregion
    }
}
