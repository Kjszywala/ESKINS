using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
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

        #endregion

        #region Constructor

        public BuyCartController(
            IItemsServices _itemsServices,
            IErrorLogsServices _errorLogsServices,
            ICartLogic _cartLogic,
            ICartServices _cartServices)
        {
            itemsServices = _itemsServices;
            errorLogsServices = _errorLogsServices;
            cartLogic = _cartLogic;
            cartServices = _cartServices;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var list = cartServices.GetAllAsync().Result;
                foreach(var item in list) { 
                    item.Item = itemsServices.GetAsync(item.ItemId.Value).Result;
                }
                return View(list);
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

        public async Task<IActionResult> AddToCartFromDetails(int id)
        {
            try
            {
                var item = cartLogic.AddToCart(id).Result;
                if (!item)
                {
                    await errorLogsServices.Add("Could not add the item to cart");
                }
                return RedirectToAction("Details", "Market");
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
