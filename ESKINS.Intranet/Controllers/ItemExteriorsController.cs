using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class ItemExteriorsController : Controller
    {
        #region Variables

        IExteriorsServices exteriorsServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public ItemExteriorsController(
            IExteriorsServices _exteriorsServices,
            IErrorLogsServices _errorLogsServices)
        {
            exteriorsServices = _exteriorsServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Controllers

        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await exteriorsServices.GetAllAsync();
                if (model == null)
                {
                    return View("Error");
                }
                return View(model);
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetExteriorAsync(int id)
        {
            var exteriors = await exteriorsServices.GetAsync(id);

            if (exteriors == null)
            {
                return NotFound();
            }

            var exteriorsModel = new ExteriorsModels
            {
                Id = exteriors.Id,
                Title = exteriors.Title,
                IsActive = exteriors.IsActive,
                CreationDate = exteriors.CreationDate,
                ModificationDate = exteriors.ModificationDate,
                Exterior = exteriors.Exterior
            };

            return Ok(exteriorsModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExteriorsModels model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await exteriorsServices.AddAsync(model);
                    if (IsConfirmed)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View("Error");
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, ExteriorsModels model)
        {
            try
            {
                var oldModel = await exteriorsServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await exteriorsServices.EditAsync(id, model);
                if (IsConfirmed)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var IsConfirmed = await exteriorsServices.RemoveAsync(id);
                if (IsConfirmed)
                {
                    return RedirectToAction("Index");
                }
                return View("Error");
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        #endregion
    }
}
