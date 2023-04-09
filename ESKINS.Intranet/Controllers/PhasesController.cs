using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class PhasesController : Controller
    {
        #region Variables

        IPhasesServices phasesServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public PhasesController(
            IPhasesServices _phasesServices,
            IErrorLogsServices _errorLogsServices)
        {
            phasesServices = _phasesServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Controllers

        // GET: CategoriesController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await phasesServices.GetAllAsync();
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
        public async Task<IActionResult> GetPhaseAsync(int id)
        {
            var phasesMethod = await phasesServices.GetAsync(id);

            if (phasesMethod == null)
            {
                return NotFound();
            }

            var phasesMethodModel = new PhasesModels
            {
                Id = phasesMethod.Id,
                Title = phasesMethod.Title,
                IsActive = phasesMethod.IsActive,
                CreationDate = phasesMethod.CreationDate,
                ModificationDate = phasesMethod.ModificationDate,
                Phase = phasesMethod.Phase
            };

            return Ok(phasesMethodModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhasesModels model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await phasesServices.AddAsync(model);
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

        // POST: CategoriesController/Edit/5
        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, PhasesModels model)
        {
            try
            {
                var oldModel = await phasesServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await phasesServices.EditAsync(id, model);
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

        // GET: CategoriesController/Delete/5
        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var IsConfirmed = await phasesServices.RemoveAsync(id);
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

            #endregion

        }
    }
}
