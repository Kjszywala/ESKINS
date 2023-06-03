using ESKINS.BusinessLogic.Interfaces;
using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
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
        IUsersServices usersServices;
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
            IExteriorsServices _exteriorsServices,
            IUsersServices _usersServices)
        {
            itemsServices = _itemsServices;
            errorLogsServices = _errorLogsServices;
            cartLogic = _cartLogic;
            cartServices = _cartServices;
            categoriesServices = _categoriesServices;
            qualitiesServices = _qualitiesServices;
            exteriorsServices = _exteriorsServices;
            usersServices = _usersServices;
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
                var product = itemsServices.GetAsync(id).Result;
                var itemList = itemsServices.GetAllAsync().Result;
                var cartList = cartServices.GetAllAsync().Result.Where(i=>i.SessionId == Config.SessionId).ToList();
                foreach(var item2 in itemList)
                {
                    foreach (var item3 in cartList)
					{
						if (id == item3.ItemId)
						{
							TempData["Message"] = "Item already in cart";
							return RedirectToAction("Index", "Market");
						}
					}
				}
				var item = cartLogic.AddToCart(id).Result;
				Config.CartOverall += product.ActualPrice;
                Config.Discount = Config.Discount + (product.ActualPrice * product.Discount);
                Config.CartItems += 1;
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
                var list = cartServices.GetAllAsync().Result;
                foreach(var i in list)
                {
                    if(i.ItemId == id && i.SessionId == Config.SessionId)
                    {
                        await cartLogic.RemoveFromCart(i.Id);
                        var item = itemsServices.GetAsync(id).Result;
                        Config.CartItems -= 1;
                        Config.Discount = Config.Discount - (item.ActualPrice * item.Discount);
                        Config.CartOverall = Config.CartOverall - item.ActualPrice;
                    }
                }
                return RedirectToAction("Index", "BuyCart"); 
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
                return RedirectToAction("Index", "Market");
            }
            catch (Exception ex)
            {
                await errorLogsServices.Error(ex);
                return View("Error");
            }
        }

		public async Task<IActionResult> Pay(string password)
        {
			try
			{
				var user = usersServices.GetAsync(Config.UserId).Result;

                if (password != user.Password)
                {
                    TempData["Message"] = "Password does not match.";
                    return RedirectToAction("Index", "BuyCart");
                }
                if (user.AccountBalance < Config.CartOverall)
				{
                    TempData["Message"] = "Not enough money in your wallet.\nPlease charge your wallet.";
                    return RedirectToAction("Index", "BuyCart");
                }

                return RedirectToAction("Index", "BuyCart");
            }
			catch (Exception ex)
			{
				await errorLogsServices.Error(ex);
				return View("Error");
			}
		}

        public async Task<IActionResult> PayOthers()
        {
            try
            {
                var cartList = cartServices.GetAllAsync().Result;
                
                foreach (var cart in cartList)
                {
                    var item = itemsServices.GetAsync(cart.ItemId.Value).Result;

                }
                return RedirectToAction("Index", "BuyCart");
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
