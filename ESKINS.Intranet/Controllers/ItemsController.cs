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
                ModificationDate = exteriors.ModificationDate,
                SerialNumber = exteriors.SerialNumber,
                ActualPrice = exteriors.ActualPrice,
                Category = exteriors.Category,
                CategoryId = exteriors.CategoryId,
                Discount = exteriors.Discount,
                Exterior = exteriors.Exterior,
                ExteriorId = exteriors.ExteriorId,
                ItemCollection = exteriors.ItemCollection,
                ItemCollectionId = exteriors.ItemCollectionId,
                ItemFloat = exteriors.ItemFloat,
                ItemImage = exteriors.ItemImage,
                ItemLocation = exteriors.ItemLocation,
                ItemLocationId = exteriors.ItemLocationId,
                ItemLog = exteriors.ItemLog,
                ItemLogsId = exteriors.ItemLogsId,
                ItemPriceHistories = exteriors.ItemPriceHistories,
                Notes = exteriors.Notes,
                OnSale = exteriors.OnSale,
                Pattern = exteriors.Pattern,
                Phase = exteriors.Phase,
                PhaseId = exteriors.PhaseId,
                ProductName = exteriors.ProductName,
                Quality = exteriors.Quality,
                QualityId = exteriors.QualityId,
                Souvenir = exteriors.Souvenir,
                StatTrack = exteriors.StatTrack,
                User = exteriors.User,
                UserId = exteriors.UserId
            };
            //ViewBag.ItemLogsList = new SelectList(await itemLogsServices.GetAllAsync(), "Id", "Title");
            //ViewBag.ItemCollectionList = new SelectList(await itemCollectionsServices.GetAllAsync(), "Id", "ItemCollection");
            //ViewBag.UserList = new SelectList(await usersServices.GetAllAsync(), "Id", "Email");
            //ViewBag.ItemLocationList = new SelectList(await itemLocationsServices.GetAllAsync(), "Id", "ItemLocation");
            //ViewBag.CategoryList = new SelectList(await categoriesServices.GetAllAsync(), "Id", "CategoryDescription");
            //ViewBag.PhaseList = new SelectList(await phasesServices.GetAllAsync(), "Id", "Phase");
            //ViewBag.QualityList = new SelectList(await qualitiesServices.GetAllAsync(), "Id", "Quality");
            //ViewBag.ExteriorList = new SelectList(await exteriorsServices.GetAllAsync(), "Id", "Exterior");

            return Ok(exteriors);
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
