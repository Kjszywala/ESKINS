using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using ESKINS.DbServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace ESKINS.Controllers
{
    public class BuyCartController : Controller
    {
        #region Properties

        IItemsServices itemsServices;
        IErrorLogsServices errorLogsServices;
        ICartLogic cartLogic;
        ICartServices cartServices;
        IExteriorsServices exteriorsServices;
        ICategoriesServices categoriesServices;
        IQualitiesServices qualitiesServices;
        public static List<ItemsModels>? itemsModels;

        #endregion

        #region Constructor

        public BuyCartController(
            IItemsServices _itemsServices,
            IErrorLogsServices _errorLogsServices,
            ICartLogic _cartLogic,
            ICartServices _cartServices,
            ICategoriesServices _categoriesServices,
            IQualitiesServices _qualitiesServices,
            IExteriorsServices _exteriorsServices)
        {
            itemsServices = _itemsServices;
            errorLogsServices = _errorLogsServices;
            cartLogic = _cartLogic;
            cartServices = _cartServices;
            categoriesServices = _categoriesServices;
            qualitiesServices = _qualitiesServices;
            exteriorsServices = _exteriorsServices;

        }

        #endregion

        #region Methods

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                itemsModels = new List<ItemsModels>();
                var list = cartServices.GetAllAsync().Result;
                var filteredList = list.Where(item=>item.SessionId == Config.SessionId).ToList();
                foreach (var item in filteredList)
                {
                    itemsModels.Add(await itemsServices.GetAsync(item.ItemId.Value));
                }
                foreach (var item in itemsModels)
                {
                    item.Category = categoriesServices.GetAsync(item.CategoryId.Value).Result;
                    item.Quality = qualitiesServices.GetAsync(item.QualityId.Value).Result;
                    item.Exterior = exteriorsServices.GetAsync(item.ExteriorId.Value).Result;
                }
                return View(itemsModels);
            }
            catch (Exception ex)
            {
                await errorLogsServices.Error(ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            try
            {
                var item = cartLogic.AddToCart(id).Result;
                if (!item)
                {
                    await errorLogsServices.Add("Could not add the item to cart");
                }
                return RedirectToAction("Index", "Market");
            }
            catch (Exception ex)
            {
                await errorLogsServices.Error(ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            try
            {
                var item = cartLogic.RemoveFromCart(id).Result;
                if (!item)
                {
                    await errorLogsServices.Add("Could not remove item from cart");
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                await errorLogsServices.Error(ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> RemoveAll(int id)
        {
            try
            {
                var item = cartLogic.RemoveAll().Result;
                if (!item)
                {
                    await errorLogsServices.Add("Could not remove all items");
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                await errorLogsServices.Error(ex);
                return View("Error");
            }
        }
        #endregion
    }
}
