using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class ItemLocationsController : Controller
    {
        #region Variables

        IItemLocationsServices locationsServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public ItemLocationsController(
            IItemLocationsServices _locationsServices,
            IErrorLogsServices _errorLogsServices)
        {
            locationsServices = _locationsServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Controllers

        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await locationsServices.GetAllAsync();
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
        public async Task<IActionResult> GetLocationAsync(int id)
        {
            var locations = await locationsServices.GetAsync(id);

            if (locations == null)
            {
                return NotFound();
            }

            var locationsModel = new ItemLocationsModels
            {
                Id = locations.Id,
                Title = locations.Title,
                IsActive = locations.IsActive,
                CreationDate = locations.CreationDate,
                ModificationDate = locations.ModificationDate,
                ItemLocation = locations.ItemLocation
            };

            return Ok(locationsModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemLocationsModels model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await locationsServices.AddAsync(model);
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
        public async Task<IActionResult> EditAsync(int id, ItemLocationsModels model)
        {
            try
            {
                var oldModel = await locationsServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await locationsServices.EditAsync(id, model);
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
                var IsConfirmed = await locationsServices.RemoveAsync(id);
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
