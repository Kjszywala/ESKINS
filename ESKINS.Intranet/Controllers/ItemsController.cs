using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESKINS.Intranet.Controllers
{
    public class ItemsController : Controller
    {
        #region Variables

        IItemsServices itemsServices;
        ICategoriesServices categoriesServices;
        IErrorLogsServices errorLogsServices;
        IItemLocationsServices itemLocationsServices;

        #endregion

        #region Constructor

        public ItemsController(
            IItemsServices _itemsServices,
            IErrorLogsServices _errorLogsServices,
            ICategoriesServices _categoriesServices,
            IItemLocationsServices itemLocationsServices)
        {
            itemsServices = _itemsServices;
            errorLogsServices = _errorLogsServices;
            categoriesServices = _categoriesServices;
            this.itemLocationsServices = itemLocationsServices;
        }

        #endregion

        #region Controllers

        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await itemsServices.GetAllAsync();
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
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var exteriors = await itemsServices.GetAsync(id);

            if (exteriors == null)
            {
                return NotFound();
            }

            var exteriorsModel = new ItemsModels
            {
                Id = exteriors.Id,
                Title = exteriors.Title,
                IsActive = exteriors.IsActive,
                CreationDate = exteriors.CreationDate,
                ModificationDate = exteriors.ModificationDate
            };

            return Ok(exteriorsModel);
        }

        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.ItemLocationList = new SelectList(await itemLocationsServices.GetAllAsync(), "Id", "ItemLocation");
            ViewBag.CategoryList = new SelectList(await categoriesServices.GetAllAsync(), "Id", "CategoryDescription");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemsModels model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await itemsServices.AddAsync(model);
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
        public async Task<IActionResult> EditAsync(int id, ItemsModels model)
        {
            try
            {
                var oldModel = await itemsServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await itemsServices.EditAsync(id, model);
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
                var IsConfirmed = await itemsServices.RemoveAsync(id);
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
