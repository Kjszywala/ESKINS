using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class ItemQualitiesController : Controller
    {
        #region Variables

        IQualitiesServices qualitiesServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public ItemQualitiesController(
            IQualitiesServices _qualitiesServices,
            IErrorLogsServices _errorLogsServices)
        {
            qualitiesServices = _qualitiesServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Controllers

        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await qualitiesServices.GetAllAsync();
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
        public async Task<IActionResult> GetQualityAsync(int id)
        {
            var exteriors = await qualitiesServices.GetAsync(id);

            if (exteriors == null)
            {
                return NotFound();
            }

            var exteriorsModel = new Qualities
            {
                Id = exteriors.Id,
                Title = exteriors.Title,
                IsActive = exteriors.IsActive,
                CreationDate = exteriors.CreationDate,
                ModificationDate = exteriors.ModificationDate,
                Quality = exteriors.Quality
            };

            return Ok(exteriorsModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Qualities model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await qualitiesServices.AddAsync(model);
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
        public async Task<IActionResult> EditAsync(int id, Qualities model)
        {
            try
            {
                var oldModel = await qualitiesServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await qualitiesServices.EditAsync(id, model);
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
                var IsConfirmed = await qualitiesServices.RemoveAsync(id);
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
