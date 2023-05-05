using ESKINS.DbServices.Interfaces;
using ESKINS.DbServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ESKINS.Intranet.Controllers
{
    public class OrdersController : Controller
    {
        #region Variables

        IOrdersServices orderServices;
        ICustomersServices customersServices;
        ISellersServices sellersServices;
        IErrorLogsServices errorLogsServices;
        IItemsServices itemsServices;
        IUsersServices usersServices;

        #endregion

        #region Constructor

        public OrdersController(
            IOrdersServices _orderServices,
            ICustomersServices _customersServices,
            ISellersServices _sellersServices,
            IErrorLogsServices _errorLogsServices,
            IItemsServices _itemsServices,
            IUsersServices _usersServices)
        {
            orderServices = _orderServices;
            customersServices = _customersServices;
            sellersServices = _sellersServices;
            errorLogsServices = _errorLogsServices;
            itemsServices = _itemsServices;
            usersServices = _usersServices;
        }

        #endregion

        #region Controllers

        // GET: CategoriesController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var model = await orderServices.GetAllAsync();
                foreach (var item in model)
                {
                    item.Customer = await customersServices.GetAsync(item.CustomerId.Value);
                    item.Seller = await sellersServices.GetAsync(item.SellerId.Value);
                    item.Item = await itemsServices.GetAsync(item.ItemId.Value);
                    item.Customer.User = await usersServices.GetAsync(item.Customer.UserId);
                    item.Seller.Users = await usersServices.GetAsync(item.Seller.UserId.Value);
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
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var sellerMethod = await orderServices.GetAsync(id);

            if (sellerMethod == null)
            {
                return NotFound();
            }

            return Ok(sellerMethod);
        }

        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                ViewBag.NameCustomer = new SelectList(await customersServices.GetAllAsync(), "Id", "FirstName");
                ViewBag.NameSeller = new SelectList(await sellersServices.GetAllAsync(), "Id", "Title");
                ViewBag.SerialNumber = new SelectList(await itemsServices.GetAllAsync(), "Id", "SerialNumber");
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
        public async Task<IActionResult> Create(OrdersModels model)
        {
            try
            {
                model.CreationDate = DateTime.Now;
                model.ModificationDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var IsConfirmed = await orderServices.AddAsync(model);
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

        public async Task<ActionResult> EditAsync(int id)
        {
            try
            {
                var model = orderServices.GetAsync(id);

                ViewBag.NameCustomer = new SelectList(await customersServices.GetAllAsync(), "Id", "FirstName");
                ViewBag.NameSeller = new SelectList(await sellersServices.GetAllAsync(), "Id", "Title");
                ViewBag.SerialNumber = new SelectList(await itemsServices.GetAllAsync(), "Id", "SerialNumber");
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
        public async Task<IActionResult> EditAsync(int id, OrdersModels model)
        {
            try
            {
                var oldModel = await orderServices.GetAsync(id);
                model.ModificationDate = DateTime.Now;
                model.CreationDate = oldModel.CreationDate;
                var IsConfirmed = await orderServices.EditAsync(id, model);
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
                var IsConfirmed = await orderServices.RemoveAsync(id);
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
