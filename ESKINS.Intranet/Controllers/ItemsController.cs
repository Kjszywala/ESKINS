using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        IUsersServices usersServices;
        IItemLogsServices itemLogsServices;
        IItemCollectionsServices itemCollectionsServices;
        IPhasesServices phasesServices;
        IQualitiesServices qualitiesServices;
        IExteriorsServices exteriorsServices;
        IItemLogic itemLogic;

        #endregion

        #region Constructor

        public ItemsController(
            IItemsServices _itemsServices,
            IErrorLogsServices _errorLogsServices,
            ICategoriesServices _categoriesServices,
            IItemLocationsServices _itemLocationsServices,
            IUsersServices _usersServices,
            IItemLogsServices _itemLogsServices,
            IItemCollectionsServices _itemCollectionsServices,
            IPhasesServices _phasesServices,
            IQualitiesServices _qualitiesServices,
            IExteriorsServices _exteriorsServices,
            IItemLogic _itemLogic
            )
        {
            itemsServices = _itemsServices;
            errorLogsServices = _errorLogsServices;
            categoriesServices = _categoriesServices;
            itemLocationsServices = _itemLocationsServices;
            usersServices = _usersServices;
            itemLogsServices = _itemLogsServices;
            itemCollectionsServices = _itemCollectionsServices;
            phasesServices = _phasesServices;
            qualitiesServices = _qualitiesServices;
            exteriorsServices = _exteriorsServices;
            itemLogic = _itemLogic;
        }

        #endregion

        #region Controllers

        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await itemsServices.GetAllAsync();
                foreach (var item in model)
                {
                    item.Category = await categoriesServices.GetAsync(item.CategoryId.Value);
                    item.ItemLocation = await itemLocationsServices.GetAsync(item.ItemLocationId.Value);
                    item.ItemCollection = await itemCollectionsServices.GetAsync(item.ItemCollectionId.Value);
                    item.Phase = await phasesServices.GetAsync(item.PhaseId.Value);
                    item.Quality = await qualitiesServices.GetAsync(item.QualityId.Value);
                    item.Exterior = await exteriorsServices.GetAsync(item.ExteriorId.Value);
                    item.User = await usersServices.GetAsync(item.UserId.Value);
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

        public async Task<ActionResult> Index2Async(string serialNumber)
        {
            try
            {
                var model = await itemsServices.GetAllAsync();
                foreach (var item2 in model)
                {
                    item2.Category = await categoriesServices.GetAsync(item2.CategoryId.Value);
                    item2.ItemLocation = await itemLocationsServices.GetAsync(item2.ItemLocationId.Value);
                    item2.ItemCollection = await itemCollectionsServices.GetAsync(item2.ItemCollectionId.Value);
                    item2.Phase = await phasesServices.GetAsync(item2.PhaseId.Value);
                    item2.Quality = await qualitiesServices.GetAsync(item2.QualityId.Value);
                    item2.Exterior = await exteriorsServices.GetAsync(item2.ExteriorId.Value);
                    item2.User = await usersServices.GetAsync(item2.UserId.Value);
                }
                if (string.IsNullOrEmpty(serialNumber))
                {
                    return View("Index", model);
                }

                var item = model.Where(x => x.SerialNumber == serialNumber).ToList();

                if (item == null)
                {
                    return View("Error");
                }

                return View("Index", item);
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
                var model = itemsServices.GetAsync(id);

                ViewBag.ItemLogsList = new SelectList(await itemLogsServices.GetAllAsync(), "Id", "Title");
                ViewBag.ItemCollectionList = new SelectList(await itemCollectionsServices.GetAllAsync(), "Id", "ItemCollection");
                ViewBag.UserList = new SelectList(await usersServices.GetAllAsync(), "Id", "Email");
                ViewBag.ItemLocationList = new SelectList(await itemLocationsServices.GetAllAsync(), "Id", "ItemLocation");
                ViewBag.CategoryList = new SelectList(await categoriesServices.GetAllAsync(), "Id", "CategoryDescription");
                ViewBag.PhaseList = new SelectList(await phasesServices.GetAllAsync(), "Id", "Phase");
                ViewBag.QualityList = new SelectList(await qualitiesServices.GetAllAsync(), "Id", "Quality");
                ViewBag.ExteriorList = new SelectList(await exteriorsServices.GetAllAsync(), "Id", "Exterior");
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

        [HttpGet]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var item = await itemsServices.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                ViewBag.ItemLogsList = new SelectList(await itemLogsServices.GetAllAsync(), "Id", "Title");
                ViewBag.ItemCollectionList = new SelectList(await itemCollectionsServices.GetAllAsync(), "Id", "ItemCollection");
                ViewBag.UserList = new SelectList(await usersServices.GetAllAsync(), "Id", "Email");
                ViewBag.ItemLocationList = new SelectList(await itemLocationsServices.GetAllAsync(), "Id", "ItemLocation");
                ViewBag.CategoryList = new SelectList(await categoriesServices.GetAllAsync(), "Id", "CategoryDescription");
                ViewBag.PhaseList = new SelectList(await phasesServices.GetAllAsync(), "Id", "Phase");
                ViewBag.QualityList = new SelectList(await qualitiesServices.GetAllAsync(), "Id", "Quality");
                ViewBag.ExteriorList = new SelectList(await exteriorsServices.GetAllAsync(), "Id", "Exterior");
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
        public async Task<IActionResult> Create(ItemsModels model)
        {
            try
            {
                //Karambit Doppler
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                var nextSerialNumber = itemLogic.GetNextSerialNumber().ToString();
                model.SerialNumber = nextSerialNumber;
                var file = Request.Form.Files.FirstOrDefault();
                if (file != null && file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        using (var binaryReader = new BinaryReader(stream))
                        {
                            var imageData = binaryReader.ReadBytes((int)file.Length);
                            model.ItemImage = imageData;
                        }
                    }
                }// image, serialnumber
                foreach (var entry in ModelState)
                {
                    string propertyName = entry.Key;
                    ModelStateEntry propertyState = entry.Value;

                    if (propertyState.ValidationState == ModelValidationState.Invalid)
                    {
                        string errorMessage = propertyState.Errors.FirstOrDefault()?.ErrorMessage;
                        await errorLogsServices.Add($"{entry.Key}: {model.SerialNumber} is invalid. Value: {entry.Value}\n{errorMessage}");
                    }
                }
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await itemsServices.AddAsync(model);
                    if (IsConfirmed)
                    {
                        return RedirectToAction("Index");
                    }
                }
                await errorLogsServices.Add("Could not add item to db table.");
                return View("Error");
            }
            catch (Exception e)
            {
                await errorLogsServices.Error(e);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditItemAsync(int ids, ItemsModels model)
        {
            try
            {
                var oldModel = await itemsServices.GetAsync(ids);
                var file = Request.Form.Files.FirstOrDefault();
                if (file != null && file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        using (var binaryReader = new BinaryReader(stream))
                        {
                            var imageData = binaryReader.ReadBytes((int)file.Length);
                            model.ItemImage = imageData;
                        }
                    }
                }
                else
                {
                    model.ItemImage = oldModel.ItemImage;
                }
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                foreach (var modelStateEntry in ModelState)
                {
                    var propertyName = modelStateEntry.Key;
                    var propertyValue = modelStateEntry.Value;

                    if (propertyValue.ValidationState == ModelValidationState.Valid)
                    {
                        // Property is valid, continue with your logic here
                    }
                    else
                    {
                        // Property is invalid, handle the error here
                    }
                }
                var IsConfirmed = await itemsServices.EditAsync(ids, model);
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
