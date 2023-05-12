﻿using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
	public class MarketController : Controller
	{
        #region Variables

        IItemLogic itemLogic;
        IItemsServices itemServices;
        IErrorLogsServices errorLogs;
        ICategoriesServices categoriesServices;
        IErrorLogsServices errorLogsServices;
        IItemLocationsServices itemLocationsServices;
        IUsersServices usersServices;
        IItemLogsServices itemLogsServices;
        IItemCollectionsServices itemCollectionsServices;
        IPhasesServices phasesServices;
        IQualitiesServices qualitiesServices;
        IExteriorsServices exteriorsServices;

        #endregion

        #region MyRegion
        public MarketController(
            IItemLogic _itemLogic,
            IErrorLogsServices _errorLogs,
            IItemsServices _itemsServices,
            ICategoriesServices _categoriesServices,
            IItemLocationsServices _itemLocationsServices,
            IUsersServices _usersServices,
            IItemLogsServices _itemLogsServices,
            IItemCollectionsServices _itemCollectionsServices,
            IPhasesServices _phasesServices,
            IQualitiesServices _qualitiesServices,
            IExteriorsServices _exteriorsServices
            )
        {
            itemLogic = _itemLogic;
            errorLogs = _errorLogs;
            itemServices = _itemsServices;
            categoriesServices = _categoriesServices;
            itemLocationsServices = _itemLocationsServices;
            usersServices = _usersServices;
            itemLogsServices = _itemLogsServices;
            itemCollectionsServices = _itemCollectionsServices;
            phasesServices = _phasesServices;
            qualitiesServices = _qualitiesServices;
            exteriorsServices = _exteriorsServices;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                //if (!Config.isConfirmed) 
                //{
                //	ViewBag.ErrorMessage = "To access this bookmark, you need to log in.";
                //	return View("/Views/Account/Index.cshtml");
                //}
                var model = itemLogic.GetBestDeals();
                return View(model);
            }
            catch (Exception ex)
            {
                await errorLogs.Error(ex);
                return View("Index");
            }

        }

        public async Task<IActionResult> SortAsync(string value) 
        {
            try
            {
                var model = new List<ItemsModels>();
                switch (value)
                {
                    case "Recomended":
                        {
                            model = itemLogic.GetBestDeals();
                            break;
                        }
                    case "Newest":
                        {
                            model = itemLogic.GetNewestFirst();
                            break;
                        }
                    case "Oldest":
                        {
                            model = itemLogic.GetOldestFirst();
                            break;
                        }
                    case "LowerPrice":
                        {
                            model = itemLogic.GetLowestPriceFirst();
                            break;
                        }
                    case "HigherPrice":
                        {
                            model = itemLogic.GetHighestPriceFirst();
                            break;
                        }
                    default:
                    {
                        return View();
                    }
                }
                return View("Index", model);
            }
            catch (Exception ex)
            {
                await errorLogs.Error(ex);
                return View("Index");
            }
        }

		[HttpPost]
		public async Task<IActionResult> DetailsAsync(ItemsModels item)
		{
            var model = itemServices.GetAsync(item.Id).Result;

            model.Category = await categoriesServices.GetAsync(model.CategoryId.Value);
            model.ItemLocation = await itemLocationsServices.GetAsync(model.ItemLocationId.Value);
            model.ItemCollection = await itemCollectionsServices.GetAsync(model.ItemCollectionId.Value);
            model.Phase = await phasesServices.GetAsync(model.PhaseId.Value);
            model.Quality = await qualitiesServices.GetAsync(model.QualityId.Value);
            model.Exterior = await exteriorsServices.GetAsync(model.ExteriorId.Value);
            model.User = await usersServices.GetAsync(model.UserId.Value);
            
            return PartialView(model);
		}

		#endregion
	}
}
