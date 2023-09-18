using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models.CMS;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class CategoriesController : Controller
    {
        #region Variables

        ICategoriesServices categoriesServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public CategoriesController(
            ICategoriesServices _categoriesServices,
            IErrorLogsServices _errorLogsServices)
        {
            categoriesServices = _categoriesServices;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Controllers

        // GET: CategoriesController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await categoriesServices.GetAllAsync();
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
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var paymentMethod = await categoriesServices.GetAsync(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            var paymentMethodModel = new Categories
            {
                Id = paymentMethod.Id,
                Title = paymentMethod.Title,
                IsActive = paymentMethod.IsActive,
                CreationDate = paymentMethod.CreationDate,
                ModificationDate = paymentMethod.ModificationDate,
                CategoryDescription = paymentMethod.CategoryDescription
            };

            return Ok(paymentMethodModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await categoriesServices.AddAsync(model);
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
        public async Task<IActionResult> EditAsync(int id, Categories model)
        {
            try
            {
                var oldModel = await categoriesServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await categoriesServices.EditAsync(id, model);
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
                var IsConfirmed = await categoriesServices.RemoveAsync(id);
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
