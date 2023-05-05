using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESKINS.Intranet.Controllers
{
    public class ItemPriceHistoryController : Controller
    {
        #region Variables

        IItemPriceHistoriesServices itemPriceHistoryService;
        IErrorLogsServices errorLogsServices;
        IItemsServices itemServices;

        #endregion

        #region Constructor

        public ItemPriceHistoryController(
            IItemPriceHistoriesServices _itemPriceHistoryService,
            IErrorLogsServices _errorLogsServices,
            IItemsServices _itemServices)
        {
            itemPriceHistoryService = _itemPriceHistoryService;
            errorLogsServices = _errorLogsServices;
            itemServices = _itemServices;
        }

        #endregion

        #region Controllers

        // GET: CategoriesController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await itemPriceHistoryService.GetAllAsync();
                foreach (var item in model)
                {
                    item.Item = await itemServices.GetAsync(item.ItemId);
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
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var itemPriceHistoryMethod = await itemPriceHistoryService.GetAsync(id);

            if (itemPriceHistoryMethod == null)
            {
                return NotFound();
            }

            return Ok(itemPriceHistoryMethod);
        }

        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                ViewBag.Name = new SelectList(await itemServices.GetAllAsync(), "Id", "ProductName");
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
        public async Task<IActionResult> Create(ItemPriceHistoriesModels model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                model.DateAndTime = DateTime.Now;
                   
                var IsConfirmed = await itemPriceHistoryService.AddAsync(model);
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

        public async Task<ActionResult> EditAsync(int id)
        {
            try
            {
                var model = itemPriceHistoryService.GetAsync(id);

                ViewBag.Name = new SelectList(await itemServices.GetAllAsync(), "Id", "ProductName");
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
        public async Task<IActionResult> EditAsync(int id, ItemPriceHistoriesModels model)
        {
            try
            {
                var oldModel = await itemPriceHistoryService.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await itemPriceHistoryService.EditAsync(id, model);
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
                var IsConfirmed = await itemPriceHistoryService.RemoveAsync(id);
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
