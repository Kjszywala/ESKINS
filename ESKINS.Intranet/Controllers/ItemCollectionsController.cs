using ESKINS.DbServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Intranet.Controllers
{
    public class ItemCollections : Controller
    {
        #region Variables

        IItemCollectionsServices collectionsServices;
        IErrorLogsServices errorLogsServices;

        #endregion

        #region Constructor

        public ItemCollections(
            IItemCollectionsServices _collectionsService,
            IErrorLogsServices _errorLogsServices)
        {
            collectionsServices = _collectionsService;
            errorLogsServices = _errorLogsServices;
        }

        #endregion

        #region Controllers

        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await collectionsServices.GetAllAsync();
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
        public async Task<IActionResult> GetCollectionAsync(int id)
        {
            var itemCollectionMethod = await collectionsServices.GetAsync(id);

            if (itemCollectionMethod == null)
            {
                return NotFound();
            }

            var itemCollectionModel = new ESKINS.DbServices.Models.CMS.ItemCollections
            {
                Id = itemCollectionMethod.Id,
                Title = itemCollectionMethod.Title,
                IsActive = itemCollectionMethod.IsActive,
                CreationDate = itemCollectionMethod.CreationDate,
                ModificationDate = itemCollectionMethod.ModificationDate,
                ItemCollection = itemCollectionMethod.ItemCollection
            };

            return Ok(itemCollectionModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ESKINS.DbServices.Models.CMS.ItemCollections model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await collectionsServices.AddAsync(model);
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
        public async Task<IActionResult> EditAsync(int id, ESKINS.DbServices.Models.CMS.ItemCollections model)
        {
            try
            {
                var oldModel = await collectionsServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await collectionsServices.EditAsync(id, model);
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
                var IsConfirmed = await collectionsServices.RemoveAsync(id);
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
