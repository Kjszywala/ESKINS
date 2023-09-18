using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESKINS.Intranet.Controllers
{
    public class TargetsController : Controller
    {
        #region Variables

        ITargetsServices targetService;
        IErrorLogsServices errorLogsServices;
        IUsersServices usersServices;
        IItemsServices itemsServices;
        IPhasesServices phasesServices;

        #endregion

        #region Constructor

        public TargetsController(
            ITargetsServices _targetService,
            IErrorLogsServices _errorLogsServices,
            IUsersServices _usersServices,
            IItemsServices _itemsServices,
            IPhasesServices _phaseServices)
        {
            targetService = _targetService;
            errorLogsServices = _errorLogsServices;
            usersServices = _usersServices;
            itemsServices = _itemsServices;
            phasesServices = _phaseServices;
        }

        #endregion

        #region Controllers

        // GET: CategoriesController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await targetService.GetAllAsync();
                foreach (var item in model)
                {
                    item.User = await usersServices.GetAsync(item.UserId.Value);
                    item.Item = await itemsServices.GetAsync(item.ItemId.Value);
                    item.Phase = await phasesServices.GetAsync(item.PhaseId.Value);
                }
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
        public async Task<IActionResult> GetTargetsAsync(int id)
        {
            var targetMethod = await targetService.GetAsync(id);

            if (targetMethod == null)
            {
                return NotFound();
            }

            return Ok(targetMethod);
        }

        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                ViewBag.User = new SelectList(await usersServices.GetAllAsync(), "Id", "Email");
                ViewBag.Item = new SelectList(await itemsServices.GetAllAsync(), "Id", "ProductName");
                ViewBag.Phase = new SelectList(await phasesServices.GetAllAsync(), "Id", "Phase");
                return View();
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Targets model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    foreach (var entry in ModelState)
                    {
                        string propertyName = entry.Key;
                        ModelStateEntry propertyState = entry.Value;

                        if (propertyState.ValidationState == ModelValidationState.Invalid)
                        {
                            string errorMessage = propertyState.Errors.FirstOrDefault()?.ErrorMessage;
                            await errorLogsServices.Add($"{entry.Key}: {entry.Value} is invalid. Value: {entry.Value}\n{errorMessage}");
                        }
                    }
                    var IsConfirmed = await targetService.AddAsync(model);
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

        public async Task<ActionResult> EditAsync(int id)
        {
            try
            {
                var model = targetService.GetAsync(id);

                ViewBag.User = new SelectList(await usersServices.GetAllAsync(), "Id", "Email");
                ViewBag.Item = new SelectList(await itemsServices.GetAllAsync(), "Id", "ProductName");
                ViewBag.Phase = new SelectList(await phasesServices.GetAllAsync(), "Id", "Phase");
                if (model == null)
                {
                    return View("Error");
                }
                return View(model.Result);
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, Targets model)
        {
            try
            {
                var oldModel = await targetService.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await targetService.EditAsync(id, model);
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
                var IsConfirmed = await targetService.RemoveAsync(id);
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
